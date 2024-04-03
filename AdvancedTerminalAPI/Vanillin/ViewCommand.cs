using System.Linq;

namespace Computerdores.Vanillin; 

public class ViewCommand : ASimpleCommand, ICommand {
    public string GetName() => "view";

    public string PredictArguments(string partialArgumentsText) {
        throw new System.NotImplementedException(); // TODO
    }

    protected override CommandResult Execute(string input, ITerminal terminal) {
        TerminalNode node = Util.FindNode(terminal.GetDriver().VanillaTerminal, "view",
            input.Split(' ').First());
        if (node == null) return new CommandResult("", false, false);
        terminal.GetDriver().VanillaTerminal.LoadTerminalImage(node);
        return new CommandResult(node.displayText, node.clearPreviousText, true);
    }

    public object Clone() => new ViewCommand();
}