namespace Computerdores.AdvancedTerminalAPI; 

public interface IPredictable {
    public string PredictInput(string partialInput, ITerminal terminal);
}