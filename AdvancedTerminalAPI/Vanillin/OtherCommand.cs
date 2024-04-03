namespace Computerdores.Vanillin; 

public class OtherCommand : ASimpleCommand, ICommand {
    public string GetName() => "other";

    protected override CommandResult Execute(string input, ITerminal terminal) {
        return new CommandResult(
            Util.FindNoun(terminal.GetDriver().VanillaTerminal, "other").specialKeywordResult.displayText,
            true, true
            );
    }

    public object Clone() => new OtherCommand();
}