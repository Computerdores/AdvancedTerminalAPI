namespace Computerdores; 

public interface ICommand {
    public string GetName();

    public string PredictArguments(string partialArgumentsText);

    public (string output, bool clearScreen, bool success) Execute(string finalArgumentsText, ITerminal terminal);
}