namespace Computerdores.Vanillin.Commands; 

public class OtherCommand : SimpleCommand, ICommand {
    public string GetName() => "other";

    /// <summary>
    /// This extracts and uses the string from the vanilla command.
    /// </summary>
    protected override CommandResult Execute(string input, ITerminal terminal) {
        return new CommandResult(
            Util.FindByKeyword(terminal.GetDriver().VanillaTerminal, "other").displayText,
            true, true
            );
    }

    public object Clone() => new OtherCommand();
}