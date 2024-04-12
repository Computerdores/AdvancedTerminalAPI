namespace Computerdores.Vanillin.Commands; 

public class InfoThingCommand : ICommand {
    private readonly string _name;
    
    public InfoThingCommand(string name) {
        _name = name;
    }

    public string GetName() => _name;

    public CommandResult Execute(string input, ITerminal terminal) {
        Terminal vT = terminal.GetDriver().VanillaTerminal;
        TerminalNode n = Util.FindKeyword(vT, "info").FindNoun(_name).result;
        if (n.creatureFileID != -1) n = TerminalWrapper.Get(vT).LoadNode(n);
        return n == null ? CommandResult.GENERIC_ERROR : 
            new CommandResult(Util.TextPostProcess(vT, n), n.clearPreviousText);
    }

    public ICommand CloneStateless() => new InfoThingCommand(_name);
}