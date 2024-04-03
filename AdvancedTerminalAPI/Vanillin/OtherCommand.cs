namespace Computerdores.Vanillin; 

public class OtherCommand : SimpleCommand, ICommand {
    public string GetName() => "other";

    /// <summary>
    /// This extracts and uses the string from the vanilla command.
    /// </summary>
    protected override CommandResult Execute(string input, ITerminal terminal) {
        return new CommandResult(
            Util.FindNoun(terminal.GetDriver().VanillaTerminal, "other").specialKeywordResult.displayText,
            true, true
            );
    }

    public object Clone() => new OtherCommand();
}