namespace Computerdores.AdvancedTerminalAPI; 

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

    public static readonly CommandResult IGNORE_INPUT = new() {clearScreen = false, wantsMoreInput = true};
    public static readonly CommandResult GENERIC_ERROR = new() {success = false};
}