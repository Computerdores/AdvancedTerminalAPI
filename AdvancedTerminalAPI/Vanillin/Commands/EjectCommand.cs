using System;
using System.Linq;

namespace Computerdores.Vanillin.Commands; 

public class EjectCommand : ICommand, IPredictable {

    private bool _awaitingConfirmation;
    
    public string GetName() => "eject";

    public string PredictInput(string partialInput) =>
        _awaitingConfirmation
            ? Util.PredictConfirmation(partialInput)
            : partialInput;

    /// <summary>
    /// For the vanilla implementation see: <see cref="Terminal.RunTerminalEvents"/>.
    /// </summary>
    public CommandResult Execute(string input, ITerminal terminal) {
        CommandResult result = new() {
            wantsMoreInput = true
        };

        if (!_awaitingConfirmation) {
            _awaitingConfirmation = true;
            result.output = Util.FindByKeyword(terminal.GetDriver().VanillaTerminal, "eject")
                .displayText;
            return result;
        }
        
        Terminal vT = terminal.GetDriver().VanillaTerminal;
        TerminalNode node = (
            from option in Util.FindByKeyword(vT, "eject").terminalOptions
            where string.Equals(option.noun.word, Util.PredictConfirmation(input),
                StringComparison.CurrentCultureIgnoreCase)
            select option
        ).FirstOrDefault()?.result;
        if (node == null) {
            result.wantsMoreInput = false;
        } else {
            vT.RunTerminalEvents(node);
            result.output = node.displayText;
        }
        return result;
    }


    public object Clone() => new EjectCommand();
}