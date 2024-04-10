using Computerdores.patch;

namespace Computerdores.Vanillin.Commands; 

public class ViewThingCommand : ICommand {
    private readonly string _name;

    public ViewThingCommand(string name) {
        _name = name;
    }

    public string GetName() => _name;

    public CommandResult Execute(string input, ITerminal terminal) {
        Terminal vT = terminal.GetDriver().VanillaTerminal;
        TerminalNode n = Util.FindKeyword(terminal, "view").FindNoun(_name).result;
        if (n.storyLogFileID != -1) n = TerminalPatch.AttemptLoadStoryLogFileNode(vT, n);
        else TerminalPatch.LoadNewNode(vT, n);
        return new CommandResult(Util.TextPostProcess(vT, n), n.clearPreviousText);
    }

    public ICommand CloneStateless() => new ViewThingCommand(_name);
}