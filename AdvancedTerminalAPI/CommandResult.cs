namespace Computerdores; 

public struct CommandResult {
    public string output;
    public bool clearScreen;
    public bool success;

    public CommandResult() {
        output = null;
        clearScreen = true;
        success = true;
    }
    
    public CommandResult(string output, bool clearScreen, bool success) {
        this.output = output;
        this.clearScreen = clearScreen;
        this.success = success;
    }
}