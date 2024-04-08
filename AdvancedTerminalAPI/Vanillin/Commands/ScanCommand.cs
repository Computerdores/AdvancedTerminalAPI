namespace Computerdores.Vanillin.Commands; 

public class ScanCommand : SimpleCommand, ICommand {
    public string GetName() => "scan";

    /// <summary>
    /// This extracts and uses the string from the vanilla command.
    /// It also uses the vanilla string formatting, see: <see cref="Terminal.TextPostProcess"/>.
    /// </summary>
    protected override CommandResult Execute(string input, ITerminal terminal) {
        Terminal vT = terminal.GetDriver().VanillaTerminal;
        TerminalNode node = Util.FindByKeyword(terminal.GetDriver().VanillaTerminal, "scan");
        return new CommandResult(vT.TextPostProcess(node.displayText, node), true, true);
    }

    public object Clone() => new ScanCommand();
}