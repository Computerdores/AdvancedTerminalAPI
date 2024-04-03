using System;

namespace Computerdores.Vanillin; 

public class WelcomeCommand : SimpleCommand, ICommand {
    public string GetName() => "welcome";

    protected override CommandResult Execute(string input, ITerminal terminal) {
        return new CommandResult(Util.GetSpecialNode(terminal.GetDriver().VanillaTerminal, 1).displayText
            .Replace("[currentDay]", DateTime.Now.DayOfWeek.ToString()), true, true);
    }

    public object Clone() => new WelcomeCommand();
}