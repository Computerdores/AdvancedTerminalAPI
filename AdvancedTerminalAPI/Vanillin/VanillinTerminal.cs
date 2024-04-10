using System;
using System.Collections.Generic;
using System.Linq;
using BepInEx.Logging;
using Computerdores.Vanillin.Commands;
using HarmonyLib;

namespace Computerdores.Vanillin;

public class VanillinTerminal : ITerminal {
    private InputFieldDriver _driver;

    private readonly Dictionary<string, ICommand> _commands = new();
    private readonly Dictionary<string, ICommand> _builtinCommands = new();

    private static ManualLogSource Log => Plugin.Log;

    private ICommand _currentCommand;

    public void RegisterDriver(InputFieldDriver driver) {
        _driver = driver;
        _driver.OnSubmit += OnSubmit;
        _driver.OnEnterTerminal += OnEnterTerminal;
        DebugLogNodeInfo();
        // Add Vanillin Commands
        AddBuiltinCommand(new SpecialNodeCommand("welcome", 1));
        AddBuiltinCommand(new SpecialNodeCommand("help", 13));
        AddBuiltinCommands(SimpleCommand.GetAll());
        AddBuiltinCommand(new EjectCommand());
        AddBuiltinCommand(new FlashCommand());
        AddBuiltinCommand(new PingCommand());
        AddBuiltinCommand(new ScanCommand());
        AddBuiltinCommand(new SwitchCommand());
        AddBuiltinCommand(new ViewCommand());
        foreach (TerminalKeyword terminalKeyword in _driver.VanillaTerminal.terminalNodes.allKeywords.Where(keyword => keyword.accessTerminalObjects)) {
            AddBuiltinCommand(new AccessibleObjectCommand(terminalKeyword.word));
        }
        AddBuiltinCommand(new BuyCommand());
        AddBuiltinCommand(new TransmitCommand());
        AddBuiltinCommand(new RouteCommand());
        AddBuiltinCommand(new InfoCommand());
    }

    public InputFieldDriver GetDriver() => _driver;

    private void AddBuiltinCommands(IEnumerable<ICommand> commands) {
        foreach (ICommand command in commands) {
            AddBuiltinCommand(command);
        }
    }
    private void AddBuiltinCommand(ICommand command) => AddCommand(_builtinCommands, command);
    public void AddCommand(ICommand command) => AddCommand(_commands, command);

    private void AddCommand(IDictionary<string, ICommand> commands, ICommand command) {
        commands[command.GetName()] = command;
        if (command is not IAliasable aliasable) return;
        foreach (ICommand cmd in aliasable.GetAll(this)) {
            AddCommand(commands, cmd);
        }
    }
    
    public void CopyCommandsTo(ITerminal terminal) {
        foreach (ICommand command in _commands.Values) {
            terminal.AddCommand(command);
        }
    }

    private void OnSubmit(string text) {
        string input = text;
        if (_currentCommand == null) {
            string[] words = text.Split(' ');
            input = words.Length == 1 ? "" : words.Skip(1).Join(delimiter: " ");
            _currentCommand = (ICommand)FindCommand(words[0])?.Clone();
        }
        if (_currentCommand is {} command) {
            Log.LogInfo($"Executing Command ({_currentCommand.GetName()}) for input : '{text}'");
            CommandResult result;
            try {
                result = command.Execute(input, this);
            } catch (Exception e) {
                result = new CommandResult(
                    "An Error occured while executing the command.\nPlease contact the author of the mod that the command is from.\n\n"
                );
                Log.LogInfo($"An error occurred during execution of '{_currentCommand.GetName()}': {e}");
            }
            if (result.success) {
                _driver.DisplayText(result.output, result.clearScreen);
            } else {
                Log.LogInfo($"Command execution was not successful for input ({_currentCommand.GetName()}): '{text}'");
                _driver.DisplayText(SpecialText(11), result.clearScreen);
            }
            if (!result.wantsMoreInput) _currentCommand = null;
        } else if (text != "") {
            Log.LogInfo($"Did not find Command for input: '{text}'");
            _driver.DisplayText(SpecialText(10), true);
        }
    }

    private void OnEnterTerminal(bool firstTime) {
        ICommand welcomeCommand = firstTime ? FindCommand("welcome") : FindCommand("help"); // TODO handle first time users
        Log.LogInfo("Entering Terminal"+(firstTime ? " for the first time" : "")+".");
        _driver.DisplayText(welcomeCommand?.Execute("", this).output, true);
    }
    

    private ICommand FindCommand(string command) {
        return FindCommand(_commands, command) ?? FindCommand(_builtinCommands, command);
    }
    
    private static ICommand FindCommand(Dictionary<string, ICommand> commands, string command) { // TODO use vanilla matching
        string[] keys = commands.Keys.Where(cmd => cmd.StartsWith(command)).ToArray();
        return keys.Length >= 1 ? commands[keys[0]] : null;
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