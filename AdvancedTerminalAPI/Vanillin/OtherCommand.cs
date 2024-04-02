namespace Computerdores.Vanillin; 

public class OtherCommand : ICommand {
    public string GetName() => "other";

    public string PredictArguments(string partialArgumentsText) {
        return partialArgumentsText;
    }

    public (string output, bool clearScreen, bool success) Execute(string finalArgumentsText, ITerminal terminal) {
        return (
            Util.FindNoun(terminal.GetDriver().VanillaTerminal, "other").specialKeywordResult.displayText,
            true, true
            );
    }
}