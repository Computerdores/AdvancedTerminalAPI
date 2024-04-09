namespace Computerdores.Vanillin.Commands; 

public class InfoCommand : ICommand {
    public string GetName() => "info";

    public CommandResult Execute(string input, ITerminal terminal) {
        Terminal vT = terminal.GetDriver().VanillaTerminal;
        TerminalNode n = Util.FindKeyword(vT, "info").FindNoun(input).result;
        return n == null ? CommandResult.GENERIC_ERROR : 
            new CommandResult(Util.TextPostProcess(vT, n), n.clearPreviousText);
    }

    public object Clone() => new InfoCommand();
}