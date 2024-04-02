using System.Collections.Generic;
using System.Linq;
using BepInEx.Logging;
using HarmonyLib;

namespace Computerdores.Vanillin;

public class VanillinTerminal : ITerminal {
    private InputFieldDriver _driver;

    private readonly Dictionary<string, ICommand> _commands = new();
    private readonly Dictionary<string, ICommand> _builtinCommands = new();

    private static ManualLogSource Log => Plugin.Log;

    private static readonly string[] TerminalAccessibleObjects = {
        "b3", "c1", "c2", "c7", "d6", "f2", "h5", "i1", "j6", "k9", "l0", "m6", "m9", "o5", "p1", "r2", "r4", "t2",
        "u2", "u9", "v0", "x8", "y9", "z3"
    };

    public void RegisterDriver(InputFieldDriver driver) {
        _driver = driver;
        _driver.OnSubmit += OnSubmit;
        _driver.OnEnterTerminal += OnEnterTerminal;
        DebugLogNodeInfo();
        // Add Vanillin Commands
        AddBuiltinCommand(new WelcomeCommand());
        AddBuiltinCommand(new SwitchCommand());
        AddBuiltinCommand(new ViewCommand());
        AddBuiltinCommand(new HelpCommand());
        AddBuiltinCommand(new OtherCommand());
        AddBuiltinCommand(new ScanCommand());
        foreach (string name in TerminalAccessibleObjects) {
            AddBuiltinCommand(new AccessibleObjectCommand(name));
        }
    }

    public InputFieldDriver GetDriver() => _driver;

    public void PreAwake() { }
    public void PostAwake() { }
    public void PreStart() { }
    public void PostStart() { }
    public void PreUpdate() { }
    public void PostUpdate() { }

    private void AddBuiltinCommand(ICommand command) {
        _builtinCommands[command.GetName()] = command;
    }
    public void AddCommand(ICommand command) {
        _commands[command.GetName()] = command;
    }
    public void CopyCommandsTo(ITerminal terminal) {
        foreach (ICommand command in _commands.Values) {
            terminal.AddCommand(command);
        }
    }

    private void OnSubmit(string text) {
        string[] words = text.Split(' ');
        string arguments = words.Length == 1 ? "" : words.Skip(1).Join(delimiter: " ");
        ICommand nCommand = FindCommand(words[0]);
        if (nCommand is {} command) {
            Log.LogInfo($"Found Command for word: '{words[0]}'");
            (string outp, bool clear, bool success) = command.Execute(arguments, this);
            if (success) {
                _driver.DisplayText(outp, clear);
            } else {
                Log.LogInfo($"Command execution failed for input: '{text}'");
                _driver.DisplayText(SpecialText(11), true);
            }
        } else {
            Log.LogInfo($"Did not find Command for input: '{text}'");
            _driver.DisplayText(SpecialText(10), true);
        }
    }

    private void OnEnterTerminal(bool firstTime) {
        ICommand welcomeCommand = firstTime ? FindCommand("welcome") : FindCommand("help");
        Log.LogInfo("Entering Terminal"+(firstTime ? " for the first time" : "")+".");
        _driver.DisplayText(welcomeCommand?.Execute("", this).output, true);
    }
    

    private ICommand FindCommand(string command) {
        return FindCommand(_commands, command) ?? FindCommand(_builtinCommands, command);
    }
    
    private static ICommand FindCommand(Dictionary<string, ICommand> commands, string command) {
        string[] keys = commands.Keys.Where(cmd => cmd.StartsWith(command)).ToArray();
        return keys.Length != 1 ? null : commands[keys[0]];
    }

    // purely for convenience
    private string SpecialText(int i) => Util.GetSpecialNode(_driver.VanillaTerminal, i).displayText;

    private void DebugLogNodeInfo() {
        // Special Nodes
        for (var i = 0; i < _driver.VanillaTerminal.terminalNodes.specialNodes.Count; i++) {
            Log.LogDebug($"Special Node ({i}): '" + 
                         _driver.VanillaTerminal.terminalNodes.specialNodes[i].displayText
                             .Replace("\n", "\\n") + 
                         "'");
        }
        // Keywords
        for (var i = 0; i < _driver.VanillaTerminal.terminalNodes.allKeywords.Length; i++) {
            TerminalKeyword kw = _driver.VanillaTerminal.terminalNodes.allKeywords[i];
            string displayText = kw.specialKeywordResult == null
                ? null
                : kw.specialKeywordResult.displayText?.Replace("\n", "\\n");
            Log.LogDebug($"Keyword ({i}), " + (kw.isVerb ? "Verb" : "Noun") + $" '{kw.word}'" +
                         (displayText != null ? $": '{displayText}' " : ""));
        }
    }
}