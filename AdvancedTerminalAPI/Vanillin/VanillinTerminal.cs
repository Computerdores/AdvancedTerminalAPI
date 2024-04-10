using System;
using System.Collections.Generic;
using System.Linq;
using BepInEx;
using BepInEx.Logging;
using Computerdores.Vanillin.Commands;
using HarmonyLib;

namespace Computerdores.Vanillin;

public class VanillinTerminal : ITerminal {
    private readonly InputFieldDriver _driver;

    private readonly List<ICommand> _commands = new();
    private readonly List<ICommand> _builtinCommands = new();

    private static ManualLogSource Log => AdvancedTerminalAPI.Log;

    private ICommand _currentCommand;

    public VanillinTerminal(InputFieldDriver driver) {
        _driver = driver;
        _driver.OnSubmit += OnSubmit;
        _driver.OnEnterTerminal += OnEnterTerminal;
        DebugLogNodeInfo();
        // Add Vanillin Commands
        AddBuiltinCommands(GetBuiltinCommands(GetDriver()));
    }

    public InputFieldDriver GetDriver() => _driver;

    public void AddCommand(ICommand command) => AddCommand(_commands, command);

    private void AddCommand(ICollection<ICommand> commands, ICommand command) {
        commands.Add(command);
        if (command is not IAliasable aliasable) return;
        aliasable.GetAll(this).Do(cmd => AddCommand(commands, cmd));
    }

    // ReSharper disable once MemberCanBePrivate.Global
    public static IEnumerable<ICommand> GetBuiltinCommands(InputFieldDriver driver) {
        var a = new List<ICommand> {
            new SpecialNodeCommand("welcome", 1), new SpecialNodeCommand("help", 13),
            new EjectCommand(), new FlashCommand(), new PingCommand(), new ScanCommand(), new SwitchCommand(),
            new ViewCommand(), new BuyCommand(), new TransmitCommand(), new RouteCommand(), new InfoCommand()
        };
        a.AddRange(SimpleCommand.GetAll());
        a.AddRange(AccessibleObjectCommand.GetAll(driver));
        return a;
    }

    private void AddBuiltinCommands(IEnumerable<ICommand> commands) {
        foreach (ICommand command in commands) {
            AddCommand(_builtinCommands, command);
        }
    }

    private void OnSubmit(string text) {
        string input = text;
        if (_currentCommand == null) {
            string[] words = text.Split(' ');
            input = words.Length == 1 ? "" : words.Skip(1).Join(delimiter: " ");
            _currentCommand = (ICommand)FindCommand(words[0])?.CloneStateless();
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
                _driver.DisplayText(
                    result.output.IsNullOrWhiteSpace() ? SpecialText(11) : result.output,
                    result.clearScreen
                );
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
    
    private static ICommand FindCommand(IEnumerable<ICommand> commands, string command)
        => commands.VanillaStringMatch(command, s => s.GetName());

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