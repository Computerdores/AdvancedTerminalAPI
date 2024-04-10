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
}