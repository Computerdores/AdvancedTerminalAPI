using System.Collections.Generic;

namespace Computerdores; 

public interface ITerminal {
    /// <summary>
    /// Should return the InputFieldDriver for the current level.
    /// </summary>
    public InputFieldDriver GetDriver();

    /// <summary>
    /// Add a command to the Terminal.
    /// </summary>
    public void AddCommand(ICommand command);

    /// <summary>
    /// Get Commands that are available to be executed.
    /// </summary>
    /// <param name="includeBuiltins">Whether to include commands that are Terminal Built-Ins.</param>
    public IEnumerable<ICommand> GetCommands(bool includeBuiltins);
}