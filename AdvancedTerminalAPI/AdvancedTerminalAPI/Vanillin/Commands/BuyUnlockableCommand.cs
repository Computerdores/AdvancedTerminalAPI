namespace Computerdores.AdvancedTerminalAPI.Vanillin.Commands; 

public class BuyUnlockableCommand : ICommand, IPredictable {
    private readonly string _itemName;
    
    private bool _awaitingConfirmation;
    private CompatibleNoun _item;

    public BuyUnlockableCommand(string itemName) {
        _itemName = itemName;
    }

    public string GetName() => _itemName;

    public string PredictInput(string partialInput, ITerminal terminal)
        => _awaitingConfirmation ? Util.PredictConfirmation(partialInput) : partialInput;
    
    public CommandResult Execute(string input, ITerminal terminal) {
        Terminal vT = terminal.GetDriver().VanillaTerminal;
        TerminalNode n;
        if (!_awaitingConfirmation) {
            _item = Util.FindKeyword(terminal, "buy").FindNoun(_itemName);
            // trigger the vanilla behaviour
            n = TerminalWrapper.Get(vT).LoadNode(_item.result);
            // output
            _awaitingConfirmation = (n.terminalOptions?.Length ?? 0) > 0;
            return new CommandResult(n.TextPostProcess(vT), n.clearPreviousText, true, _awaitingConfirmation);
        }
        
        CompatibleNoun cn = _item.result.FindTerminalOption(input);
        // if the input doesn't match any available option ignore it
        if (cn == null) return CommandResult.IgnoreInput;
        
        n = TerminalWrapper.Get(vT).LoadNode(cn.result);
        return new CommandResult(n.TextPostProcess(vT), n.clearPreviousText);
    }

    public ICommand CloneStateless()
        => new BuyUnlockableCommand(_itemName);
}