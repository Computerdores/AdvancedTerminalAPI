namespace Computerdores; 

public struct CommandResult {
    public string output;
    public bool clearScreen;
    public bool success;
    /// <summary>
    /// Whether the command has finished executing, or wants further input from the player.
    /// </summary>
    public bool wantsMoreInput;

    public CommandResult(string output = null, bool clearScreen = true, bool success = true, bool wantsMoreInput = false) {
        this.output = output;
        this.clearScreen = clearScreen;
        this.success = success;
        this.wantsMoreInput = wantsMoreInput;
    }
}