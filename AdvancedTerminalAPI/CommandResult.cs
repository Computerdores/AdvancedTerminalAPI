namespace Computerdores; 

public struct CommandResult {
    public string output;
    public bool clearScreen = true;
    public bool success = true;
    
    public CommandResult(string output, bool clearScreen, bool success) {
        this.output = output;
        this.clearScreen = clearScreen;
        this.success = success;
    }
}