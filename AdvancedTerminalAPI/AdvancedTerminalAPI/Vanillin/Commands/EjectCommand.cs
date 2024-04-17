namespace Computerdores.AdvancedTerminalAPI.Vanillin.Commands; 

public class EjectCommand : ICommand, IPredictable {

    private bool _awaitingConfirmation;
    
    public string GetName() => "eject";

    public string PredictInput(string partialInput, ITerminal terminal) =>
        _awaitingConfirmation ? Util.PredictConfirmation(partialInput) : partialInput;

    /// <summary>
    /// For the vanilla implementation see: <see cref="Terminal.RunTerminalEvents"/>.
    /// </summary>
    public CommandResult Execute(string input, ITerminal terminal) {
        Terminal vT = terminal.GetDriver().VanillaTerminal;

        if (!_awaitingConfirmation) {
            _awaitingConfirmation = true;
            return new CommandResult(Util.FindByKeyword(vT, "eject").displayText, wantsMoreInput: true);
        }
        
        TerminalNode node = Util.FindByKeyword(vT, "eject").FindTerminalOption(input)?.result;
        if (node == null) return CommandResult.IgnoreInput;
        node = TerminalWrapper.Get(vT).LoadNode(node);
        return new CommandResult(node.TextPostProcess(vT));
    }


    public ICommand CloneStateless() => new EjectCommand();
}