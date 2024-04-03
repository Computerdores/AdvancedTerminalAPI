namespace Computerdores.Vanillin; 

public class ScanCommand : ASimpleCommand, ICommand {
    public string GetName() => "scan";

    public string PredictArguments(string partialArgumentsText) => partialArgumentsText;

    protected override CommandResult Execute(string input, ITerminal terminal) {
        Terminal vT = terminal.GetDriver().VanillaTerminal;
        TerminalNode node = Util.FindNoun(terminal.GetDriver().VanillaTerminal, "scan").specialKeywordResult;
        return new CommandResult(vT.TextPostProcess(node.displayText, node), true, true);
    }

    public object Clone() => new ScanCommand();
}