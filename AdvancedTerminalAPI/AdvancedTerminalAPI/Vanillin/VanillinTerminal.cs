using System;
using System.Collections.Generic;
using System.Linq;
using BepInEx;
using BepInEx.Logging;
using Computerdores.AdvancedTerminalAPI.Vanillin.Commands;
using HarmonyLib;

namespace Computerdores.AdvancedTerminalAPI.Vanillin;

// ReSharper disable MemberCanBePrivate.Global
public class VanillinTerminal : ITerminal {
    protected readonly InputFieldDriver driver;
    protected readonly TerminalWrapper wrapper;

    protected List<ICommand> Commands { get; } = new();
    protected List<ICommand> BuiltinCommands { get; } = new();

    private static ManualLogSource Log => Plugin.Log;

    protected ICommand currentCommand;

    public VanillinTerminal(InputFieldDriver driver) {
        this.driver = driver;
        wrapper = TerminalWrapper.Get(driver.VanillaTerminal);
        this.driver.OnSubmit += OnSubmit;
        wrapper.EnterTerminal += OnEnterTerminal;
        //DebugLogNodeInfo();
        // Add Vanillin Commands
        AddBuiltinCommands(GetBuiltinCommands(GetDriver()));
    }

    public InputFieldDriver GetDriver() => driver;

    public virtual IEnumerable<ICommand> GetCommands(bool includeBuiltins) {
        return includeBuiltins ? BuiltinCommands.Concat(Commands) : Commands;
    }

    public void AddCommand(ICommand command) => AddCommand(Commands, command);

    protected virtual void AddCommand(ICollection<ICommand> commands, ICommand command) {
        commands.Add(command);
        if (command is not IAliasable aliasable) return;
        aliasable.GetAll(this).Do(cmd => AddCommand(commands, cmd));
    }

    // ReSharper disable once MemberCanBePrivate.Global
    public static IEnumerable<ICommand> GetBuiltinCommands(InputFieldDriver driver) {
        var a = new List<ICommand> {
            new SpecialNodeCommand("welcome", 1),
            new ViewCommand(), new SwitchCommand(), new PingCommand(), new TransmitCommand(), new ScanCommand(),
            new BuyCommand(), new RouteCommand(), new InfoCommand(), new EjectCommand(), new FlashCommand(), 
            new OtherCommand()
        };
        a.AddRange(SimpleCommand.GetAll());
        a.AddRange(AccessibleObjectCommand.GetAll(driver));
        return a;
    }

    private void AddBuiltinCommands(IEnumerable<ICommand> commands) {
        foreach (ICommand command in commands) {
            AddCommand(BuiltinCommands, command);
        }
    }

    protected virtual void OnSubmit(string text) {
        string input = text;
        if (currentCommand == null) {
            string[] words = text.Split(' ');
            input = words.Length == 1 ? "" : words.Skip(1).Join(delimiter: " ");
            currentCommand = (ICommand)FindCommand(words[0])?.CloneStateless();
        }
        if (currentCommand is {} command) {
            Log.LogInfo($"Executing Command ({currentCommand.GetName()}) for input : '{text}'");
            CommandResult result;
            try {
                result = command.Execute(input, this);
            } catch (Exception e) {
                result = new CommandResult(
                    "An Error occured while executing the command.\nPlease contact the author of the mod that the command is from.\n\n"
                );
                Log.LogInfo($"An error occurred during execution of '{currentCommand.GetName()}': {e}");
            }
            if (result.success) {
                driver.DisplayText(result.output, result.clearScreen);
            } else {
                Log.LogInfo($"Command execution was not successful for input ({currentCommand.GetName()}): '{text}'");
                driver.DisplayText(
                    result.output.IsNullOrWhiteSpace() ? SpecialText(11) : result.output,
                    result.clearScreen
                );
            }
            if (!result.wantsMoreInput) currentCommand = null;
        } else if (text != "") {
            Log.LogInfo($"Did not find Command for input: '{text}'");
            driver.DisplayText(SpecialText(10), true);
        }
    }

    protected virtual void OnEnterTerminal(bool firstTime) {
        ICommand welcomeCommand = firstTime ? FindCommand("welcome") : FindCommand("help"); // TODO handle first time users
        Log.LogInfo("Entering Terminal"+(firstTime ? " for the first time" : "")+".");
        driver.DisplayText(welcomeCommand?.Execute("", this).output, true);
    }
    

    protected virtual ICommand FindCommand(string command) {
        return FindCommand(Commands, command) ?? FindCommand(BuiltinCommands, command);
    }
    
    protected static ICommand FindCommand(IEnumerable<ICommand> commands, string command)
        => commands.VanillaStringMatch(command, s => s.GetName());

    // purely for convenience
    protected string SpecialText(int i) => Util.GetSpecialNode(driver.VanillaTerminal, i).displayText;

    private void DebugLogNodeInfo() {
        // Special Nodes
        for (var i = 0; i < driver.VanillaTerminal.terminalNodes.specialNodes.Count; i++) {
            Log.LogDebug($"Special Node ({i}): '" + 
                         driver.VanillaTerminal.terminalNodes.specialNodes[i].displayText
                             .Replace("\n", "\\n") + 
                         "'");
        }
        // Keywords
        for (var i = 0; i < driver.VanillaTerminal.terminalNodes.allKeywords.Length; i++) {
            TerminalKeyword kw = driver.VanillaTerminal.terminalNodes.allKeywords[i];
            string displayText = kw.specialKeywordResult == null
                ? null
                : kw.specialKeywordResult.displayText?.Replace("\n", "\\n");
            Log.LogDebug($"Keyword ({i}), " + (kw.isVerb ? "Verb" : "Noun") + $" '{kw.word}'" +
                         (displayText != null ? $": '{displayText}' " : ""));
        }
    }
}
// ReSharper restore MemberCanBePrivate.Global