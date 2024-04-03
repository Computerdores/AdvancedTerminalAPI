namespace Computerdores.Vanillin; 

public class AccessibleObjectCommand : ASimpleCommand, ICommand {
    private readonly string _name;

    public AccessibleObjectCommand(string name) {
        _name = name;
    }
    
    public string GetName() => _name;

    public string PredictArguments(string partialArgumentsText) => partialArgumentsText;

    protected override CommandResult Execute(string input, ITerminal terminal) {
        Terminal vT = terminal.GetDriver().VanillaTerminal;
        vT.CallFunctionInAccessibleTerminalObject(_name);
        vT.PlayBroadcastCodeEffect();
        return new CommandResult(Util.GetSpecialNode(vT, 19).displayText, true, true);
    }

    public object Clone() => new AccessibleObjectCommand(_name);
}