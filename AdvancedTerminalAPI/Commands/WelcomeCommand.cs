namespace Computerdores.Commands; 

public class WelcomeCommand : ICommand {
    public string GetName() {
        return "welcome";
    }

    public string PredictArguments(string partialArgumentsText) {
        return partialArgumentsText;
    }

    public (string output, bool clearScreen) Execute(string finalArgumentsText) {
        return ("TODO Welcome text\n", true);
    }
}