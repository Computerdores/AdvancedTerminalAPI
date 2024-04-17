namespace Computerdores.AdvancedTerminalAPI; 

// ReSharper disable FieldCanBeMadeReadOnly.Global
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

    public static readonly CommandResult IgnoreInput = new(null, false, true, true);
    public static readonly CommandResult GenericError = new(null, true, false);
}
// ReSharper restore FieldCanBeMadeReadOnly.Global