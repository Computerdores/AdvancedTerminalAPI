namespace Computerdores.AdvancedTerminalAPI.Vanillin.Commands; 

public class OtherCommand : ICommand {
    public string GetName()
        => "other";

    public CommandResult Execute(string input, ITerminal terminal) {
        var text = "Other commands:\n\n";

        foreach (ICommand command in terminal.GetCommands(true)) {
            if (command is IDescribable describable)
                text += $">{describable.GetUsage()}\n{describable.GetDescription()}\n\n";
        }

        text += "\n";
        
        return new CommandResult(text);
    }

    public ICommand CloneStateless()
        => new OtherCommand();
}