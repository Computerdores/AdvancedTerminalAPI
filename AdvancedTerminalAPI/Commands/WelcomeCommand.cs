using System;

namespace Computerdores.Commands; 

public class WelcomeCommand : ICommand {
    public string GetName() => "welcome";

    public string PredictArguments(string partialArgumentsText) {
        return partialArgumentsText;
    }

    public (string output, bool clearScreen, bool success) Execute(string finalArgumentsText, ITerminal terminal) {
        return (Util.GetSpecialNode(terminal.GetDriver().VanillaTerminal, 1).displayText
            .Replace("[currentDay]", DateTime.Now.DayOfWeek.ToString()), true, true);
    }
}