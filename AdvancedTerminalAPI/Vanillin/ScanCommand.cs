namespace Computerdores.Vanillin; 

public class ScanCommand : ICommand {
    public string GetName() => "scan";

    public string PredictArguments(string partialArgumentsText) => partialArgumentsText;

    public (string output, bool clearScreen, bool success) Execute(string finalArgumentsText, ITerminal terminal) {
        Terminal vT = terminal.GetDriver().VanillaTerminal;
        TerminalNode node = Util.FindNoun(terminal.GetDriver().VanillaTerminal, "scan").specialKeywordResult;
        return (vT.TextPostProcess(node.displayText, node), true, true);
    }
}