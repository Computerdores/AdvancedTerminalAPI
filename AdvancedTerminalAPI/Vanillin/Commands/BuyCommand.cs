using System.Collections.Generic;
using System.Linq;
using HarmonyLib;

namespace Computerdores.Vanillin.Commands; 

public class BuyCommand : ICommand {
    private bool _awaitingConfirmation;

    private ICommand _command;
    
    public string GetName() => "buy";

    public CommandResult Execute(string input, ITerminal terminal) {
        if (_awaitingConfirmation) return _command.Execute(input, terminal);
        
        _awaitingConfirmation = true;
        string[] words = input.Split(' ');
        _command = FromPlayerInput(terminal.GetDriver().VanillaTerminal, words.First());
        
        return _command?.Execute(words.Skip(1).Join(delimiter: " "), terminal) ?? CommandResult.IGNORE_INPUT;
    }

    public object Clone() => new BuyCommand();

    public static IEnumerable<ICommand> GetAll(ITerminal term) {
        return ((IEnumerable<ICommand>)BuyItemCommand.GetAll(term)).Concat(BuyUnlockableCommand.GetAll(term));
    }

    public static ICommand FromPlayerInput(Terminal term, string input) {
        CompatibleNoun a = Util.FindKeyword(term, "buy").
            FindNoun(input, cn => cn.result.shipUnlockableID != -1 || cn.result.buyItemIndex != -1);
        if (a.result.buyItemIndex != -1) {
            return new BuyItemCommand(a.noun.word);
        }
        return new BuyUnlockableCommand(a.noun.word);
    }
}