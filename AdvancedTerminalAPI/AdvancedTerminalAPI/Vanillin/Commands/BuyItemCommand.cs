using System.Text.RegularExpressions;
using BepInEx;
using UnityEngine;

namespace Computerdores.AdvancedTerminalAPI.Vanillin.Commands; 

public class BuyItemCommand : ICommand, IPredictable {
    private readonly string _itemName;
    
    private bool _awaitingConfirmation;
    private CompatibleNoun _item;

    public BuyItemCommand(string itemName) {
        _itemName = itemName;
    }

    public string GetName() => _itemName;

    public string PredictInput(string partialInput, ITerminal terminal)
        => _awaitingConfirmation ? Util.PredictConfirmation(partialInput) : partialInput;

    public CommandResult Execute(string input, ITerminal terminal) {
        Terminal vT = terminal.GetDriver().VanillaTerminal;
        TerminalNode n;
        if (!_awaitingConfirmation) {
            // determine the ordered amount and set it in Terminal
            _item = Util.FindKeyword(terminal, "buy").FindNoun(_itemName);
            string count = Regex.Match(input, "\\d+").Value;
            vT.playerDefinedAmount = count.IsNullOrWhiteSpace() ? 1 : Mathf.Clamp(int.Parse(count), 0, 10);
            // trigger the vanilla behaviour
            n = TerminalWrapper.Get(vT).LoadNode(_item.result);
            // output
            _awaitingConfirmation = n.isConfirmationNode;
            return new CommandResult(n.TextPostProcess(vT), n.clearPreviousText, true, _awaitingConfirmation);
        }

        
        CompatibleNoun cn = _item.result.FindTerminalOption(input);
        // if the input doesn't match any available option ignore it
        if (cn == null) return CommandResult.IGNORE_INPUT;
        
        n = TerminalWrapper.Get(vT).LoadNode(cn.result);
        return new CommandResult(n.TextPostProcess(vT), n.clearPreviousText);
    }

    public ICommand CloneStateless()
        => new BuyItemCommand(_itemName);
}