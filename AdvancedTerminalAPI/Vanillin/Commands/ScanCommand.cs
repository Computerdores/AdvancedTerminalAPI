namespace Computerdores.Vanillin.Commands; 

public class ScanCommand : ICommand {
    public string GetName() => "scan";

    /// <summary>
    /// This extracts and uses the string from the vanilla command.
    /// It also uses the vanilla string formatting, see: <see cref="Terminal.TextPostProcess"/>.
    /// </summary>
    public CommandResult Execute(string input, ITerminal terminal) {
        Terminal vT = terminal.GetDriver().VanillaTerminal;
        TerminalNode node = Util.FindByKeyword(terminal.GetDriver().VanillaTerminal, "scan");
        return new CommandResult(vT.TextPostProcess(node.displayText, node));
    }

    public ICommand CloneStateless() => new ScanCommand();
}