using System;

namespace Computerdores.Commands; 

public class WelcomeCommand : ICommand {
    public string GetName() {
        return "welcome";
    }

    public string PredictArguments(string partialArgumentsText) {
        return partialArgumentsText;
    }

    public (string output, bool clearScreen) Execute(string finalArgumentsText) {
        return ("Welcome to the FORTUNE-9 OS\n"+
                "          Courtesy of the Company\n\n"+
               $"Happy {DateTime.Now.DayOfWeek.ToString()}.\n\n"+
                "Type \"Help\" for a list of commands.\n\n\n\n\n", true);
    }
}