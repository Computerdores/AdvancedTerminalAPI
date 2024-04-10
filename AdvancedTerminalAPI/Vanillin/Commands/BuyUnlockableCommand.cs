using System.Collections.Generic;
using System.Linq;
using Computerdores.patch;

namespace Computerdores.Vanillin.Commands; 

public class BuyUnlockableCommand : ICommand{
    private readonly string _itemName;
    
    private bool _awaitingConfirmation;
    private CompatibleNoun _item;

    public BuyUnlockableCommand(string itemName) {
        _itemName = itemName;
    }

    public string GetName() => _itemName; 

    public CommandResult Execute(string input, ITerminal terminal) {
        Terminal vT = terminal.GetDriver().VanillaTerminal;
        TerminalNode n;
        if (!_awaitingConfirmation) {
            _item = Util.FindKeyword(terminal, "buy").FindNoun(_itemName);
            // trigger the vanilla behaviour
            n = TerminalPatch.LoadNewNodeIfAffordable(vT, _item.result);
            // output
            _awaitingConfirmation = (n.terminalOptions?.Length ?? 0) > 0;
            return new CommandResult(Util.TextPostProcess(vT, n), n.clearPreviousText, true, _awaitingConfirmation);
        }

        
        CompatibleNoun cn = _item.result.FindTerminalOption(input);
        // if the input doesn't match any available option ignore it
        if (cn == null) return CommandResult.IGNORE_INPUT;
        
        n = TerminalPatch.LoadNewNodeIfAffordable(vT, cn.result);
        return new CommandResult(Util.TextPostProcess(vT, n), n.clearPreviousText);
    }

    public object Clone()
        => new BuyUnlockableCommand(_itemName);

    public static IEnumerable<BuyUnlockableCommand> GetAll(ITerminal term) {
        return from noun in Util.FindKeyword(term, "buy").compatibleNouns 
            where noun.result.shipUnlockableID != -1
            select new BuyUnlockableCommand(noun.noun.word);
    }
}