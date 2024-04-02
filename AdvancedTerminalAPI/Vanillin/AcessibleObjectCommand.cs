namespace Computerdores.Vanillin; 

public class AccessibleObjectCommand : ICommand {
    private readonly string _name;

    public AccessibleObjectCommand(string name) {
        _name = name;
    }
    
    public string GetName() => _name;

    public string PredictArguments(string partialArgumentsText) => partialArgumentsText;

    public (string output, bool clearScreen, bool success) Execute(string finalArgumentsText, ITerminal terminal) {
        Terminal vT = terminal.GetDriver().VanillaTerminal;
        vT.CallFunctionInAccessibleTerminalObject(_name);
        vT.PlayBroadcastCodeEffect();
        return (Util.GetSpecialNode(vT, 19).displayText, true, true);
    }
}