using System.Linq;

namespace Computerdores.Vanillin; 

public class EjectCommand : ICommand, IPredictable {

    private bool _awaitingConfirmation;

    public EjectCommand() {}
    private EjectCommand(bool awaitingConfirmation) {
        _awaitingConfirmation = awaitingConfirmation;
    }
    
    public string GetName() => "eject";

    public string PredictInput(string partialInput) =>
        _awaitingConfirmation
            ? Util.PredictConfirmation(partialInput)
            : partialInput;

    public CommandResult Execute(string input, ITerminal terminal, out bool wantsMoreInput) {
        wantsMoreInput = true;
        CommandResult result = new();
        
        if (!_awaitingConfirmation) {
            _awaitingConfirmation = true;
            result.output = Util.FindNoun(terminal.GetDriver().VanillaTerminal, "eject").specialKeywordResult
                .displayText;
            return result;
        }
        
        Terminal vT = terminal.GetDriver().VanillaTerminal;
        TerminalNode node = (
            from option in Util.FindNoun(vT, "eject")
                .specialKeywordResult.terminalOptions
            where string.Equals(option.noun.word, Util.PredictConfirmation(input),
                StringComparison.CurrentCultureIgnoreCase)
            select option
        ).FirstOrDefault()?.result;
        if (node == null) {
            wantsMoreInput = false;
        } else {
            vT.RunTerminalEvents(node);
            result.output = node.displayText;
        }
        return result;
    }


    public object Clone() => new EjectCommand(_awaitingConfirmation);
}