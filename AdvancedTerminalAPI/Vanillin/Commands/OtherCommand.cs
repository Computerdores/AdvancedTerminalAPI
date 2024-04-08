namespace Computerdores.Vanillin.Commands; 

public class OtherCommand : ICommand {
    public string GetName() => "other";

    /// <summary>
    /// This extracts and uses the string from the vanilla command.
    /// </summary>
    public CommandResult Execute(string input, ITerminal terminal) {
        return new CommandResult(Util.FindByKeyword(terminal.GetDriver().VanillaTerminal, "other").displayText);
    }

    public object Clone() => new OtherCommand();
}