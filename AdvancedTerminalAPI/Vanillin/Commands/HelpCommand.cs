namespace Computerdores.Vanillin.Commands; 

public class HelpCommand : ICommand {
    public string GetName() => "help";

    /// <summary>
    /// This extracts and uses the vanilla string.
    /// It also uses the vanilla string formatting, see: <see cref="Terminal.TextPostProcess"/>.
    /// </summary>
    public CommandResult Execute(string input, ITerminal terminal) {
        Terminal vT = terminal.GetDriver().VanillaTerminal;
        TerminalNode node = Util.GetSpecialNode(vT, 13);
        return new CommandResult(vT.TextPostProcess(node.displayText, node));
    }

    public object Clone() => new HelpCommand();
}