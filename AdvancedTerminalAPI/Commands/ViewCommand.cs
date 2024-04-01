using System.Linq;

namespace Computerdores.Commands; 

public class ViewCommand : ICommand {
    public string GetName() => "view";

    public string PredictArguments(string partialArgumentsText) {
        throw new System.NotImplementedException(); // TODO
    }

    public (string output, bool clearScreen, bool success) Execute(string finalArgumentsText, ITerminal terminal) {
        TerminalNode node = Util.FindNode(terminal.GetDriver().VanillaTerminal, "view",
            finalArgumentsText.Split(' ').First());
        if (node == null) return ("", false, false);
        terminal.GetDriver().VanillaTerminal.LoadTerminalImage(node);
        return (node.displayText, node.clearPreviousText, true);
    }
}