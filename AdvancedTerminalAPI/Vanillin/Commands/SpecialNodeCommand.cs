namespace Computerdores.Vanillin.Commands; 

public class SpecialNodeCommand : ICommand {
    private readonly string _name;
    private readonly int _index;

    public SpecialNodeCommand(string name, int index) {
        _name = name;
        _index = index;
    }

    public string GetName() => _name;

    public CommandResult Execute(string input, ITerminal terminal) {
        Terminal vT = terminal.GetDriver().VanillaTerminal;
        return new CommandResult(Util.TextPostProcess(vT, Util.GetSpecialNode(vT, _index)));
    }

    public ICommand CloneStateless() => new SpecialNodeCommand(_name, _index);
}