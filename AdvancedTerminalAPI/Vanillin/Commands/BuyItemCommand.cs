using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using BepInEx;
using Computerdores.patch;
using UnityEngine;

namespace Computerdores.Vanillin.Commands; 

public class BuyItemCommand : ICommand {
    private readonly string _itemName;
    
    private bool _awaitingConfirmation;
    private CompatibleNoun _item;

    public BuyItemCommand(string itemName) {
        _itemName = itemName;
    }

    public string GetName() => _itemName; 

    public CommandResult Execute(string input, ITerminal terminal, out bool wantsMoreInput) {
        Terminal vT = terminal.GetDriver().VanillaTerminal;
        TerminalNode n;
        if (!_awaitingConfirmation) {
            // determine the ordered amount and set it in Terminal
            _item = Util.FindKeyword(terminal, "buy").FindNoun(_itemName);
            string count = Regex.Match(input, "\\d+").Value;
            vT.playerDefinedAmount = count.IsNullOrWhiteSpace() ? 1 : Mathf.Clamp(int.Parse(count), 0, 10);
            // trigger the vanilla behaviour
            n = TerminalPatch.LoadNewNodeIfAffordable(vT, _item.result);
            // output
            wantsMoreInput = _awaitingConfirmation = n.isConfirmationNode;
            return new CommandResult(Util.TextPostProcess(vT, n), n.clearPreviousText, true);
        }

        
        CompatibleNoun cn = _item.result.FindTerminalOption(input);
        // if the input doesn't match any available option ignore it
        if (cn == null) {
            wantsMoreInput = true;
            return new CommandResult(null, false, true);
        }
        wantsMoreInput = false;
        n = TerminalPatch.LoadNewNodeIfAffordable(vT, cn.result);
        return new CommandResult(Util.TextPostProcess(vT, n), n.clearPreviousText, true);
    }

    public object Clone()
        => new BuyItemCommand(_itemName);

    public static IEnumerable<BuyItemCommand> GetAll(ITerminal term) {
        return from noun in Util.FindKeyword(term, "buy").compatibleNouns 
            where noun.result.buyItemIndex != -1
            select new BuyItemCommand(noun.noun.word);
    }

    public static BuyItemCommand FromPlayerInput(Terminal term, string input) {
        return new BuyItemCommand(Util.FindKeyword(term, "buy").
            FindNoun(input, cn => cn.result.buyItemIndex != -1).noun.word
        );
    }
}