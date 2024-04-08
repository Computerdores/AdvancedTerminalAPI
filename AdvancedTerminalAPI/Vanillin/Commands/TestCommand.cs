namespace Computerdores.Vanillin.Commands; 

public class TestCommand : SimpleCommand, ICommand {
    
    public string GetName() => "test";
    
    protected override CommandResult Execute(string input, ITerminal terminal) {
        return new CommandResult("test!\n", false, true);
    }
    
    public object Clone() => new TestCommand();
}