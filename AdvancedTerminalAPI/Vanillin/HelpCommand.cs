namespace Computerdores.Vanillin; 

public class HelpCommand : SimpleCommand, ICommand {
    public string GetName() => "help";

    protected override CommandResult Execute(string input, ITerminal terminal) {
        Terminal vT = terminal.GetDriver().VanillaTerminal;
        TerminalNode node = Util.GetSpecialNode(vT, 13);
        return new CommandResult(vT.TextPostProcess(node.displayText, node), true, true);
    }

    public object Clone() => new HelpCommand();
}