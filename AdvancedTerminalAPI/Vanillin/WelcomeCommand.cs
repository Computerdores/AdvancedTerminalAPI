namespace Computerdores.Vanillin; 

public class WelcomeCommand : SimpleCommand, ICommand {
    public string GetName() => "welcome";

    /// <summary>
    /// This extracts and uses the vanilla string.
    /// It also uses the vanilla string formatting, see: <see cref="Terminal.TextPostProcess"/>.
    /// </summary>
    protected override CommandResult Execute(string input, ITerminal terminal) {
        Terminal vT = terminal.GetDriver().VanillaTerminal;
        TerminalNode node = Util.GetSpecialNode(vT, 1);
        return new CommandResult(vT.TextPostProcess(node.displayText, node), true, true);
    }

    public object Clone() => new WelcomeCommand();
}