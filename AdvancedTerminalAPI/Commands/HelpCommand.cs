namespace Computerdores.Commands; 

public class HelpCommand : ICommand {
    public string GetName() => "help";

    public string PredictArguments(string partialArgumentsText) {
        return partialArgumentsText;
    }

    public (string output, bool clearScreen, bool success) Execute(string finalArgumentsText, ITerminal terminal) {
        Terminal vT = terminal.GetDriver().VanillaTerminal;
        return (Util.GetSpecialNode(vT, 13).displayText.
            Replace("[numberOfItemsOnRoute]",
                vT.numberOfItemsInDropship > 0
                    ? $"{vT.numberOfItemsInDropship} purchased items on route."
                    : ""
                ),
            true, true);
    }
}