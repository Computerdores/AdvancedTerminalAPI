namespace Computerdores.Vanillin.Commands; 

public class TestCommand : ICommand {
    
    public string GetName() => "test";

    public CommandResult Execute(string input, ITerminal terminal) {
        return new CommandResult("test!\n", false);
    }

    public object Clone() => new TestCommand();
}