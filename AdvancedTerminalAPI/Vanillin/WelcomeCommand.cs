namespace Computerdores.Vanillin; 

public class WelcomeCommand : SimpleCommand, ICommand {
    public string GetName() => "welcome";

    protected override CommandResult Execute(string input, ITerminal terminal) {
        Terminal vT = terminal.GetDriver().VanillaTerminal;
        TerminalNode node = Util.GetSpecialNode(vT, 1);
        return new CommandResult(vT.TextPostProcess(node.displayText, node), true, true);
    }

    public object Clone() => new WelcomeCommand();
}