using System.Collections.Generic;
using System.Linq;
using HarmonyLib;

namespace Computerdores.AdvancedTerminalAPI.Vanillin.Commands; 

public class BuyCommand : ICommand, IAliasable, IPredictable {
    private bool _awaitingConfirmation;

    private ICommand _command;

    public string GetName() => "buy";

    public string PredictInput(string partialInput, ITerminal terminal) {
        return _awaitingConfirmation
            ? Util.PredictConfirmation(partialInput)
            : FromPlayerInput(terminal.GetDriver().VanillaTerminal, partialInput).GetName();
    }

    public CommandResult Execute(string input, ITerminal terminal) {
        if (_awaitingConfirmation) return _command.Execute(input, terminal);
        
        _awaitingConfirmation = true;
        string[] words = input.Split(' ');
        _command = FromPlayerInput(terminal.GetDriver().VanillaTerminal, words.First());
        
        return _command?.Execute(words.Skip(1).Join(delimiter: " "), terminal) ?? CommandResult.IgnoreInput;
    }

    public ICommand CloneStateless() => new BuyCommand();

    public IEnumerable<ICommand> GetAll(ITerminal term) {
        return from cn in Util.FindKeyword(term, "buy").compatibleNouns 
            where cn.result.shipUnlockableID != -1 || cn.result.buyItemIndex != -1
            select FromCompatibleNoun(cn);
    }

    private static ICommand FromPlayerInput(Terminal term, string input) {
        CompatibleNoun a = Util.FindKeyword(term, "buy").
            FindNoun(input, cn => cn.result.shipUnlockableID != -1 || cn.result.buyItemIndex != -1);
        return FromCompatibleNoun(a);
    }

    private static ICommand FromCompatibleNoun(CompatibleNoun cn) {
        if (cn.result.buyItemIndex != -1) {
            return new BuyItemCommand(cn.noun.word);
        }
        return new BuyUnlockableCommand(cn.noun.word);
    }
}