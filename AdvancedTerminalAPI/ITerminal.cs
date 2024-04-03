namespace Computerdores; 

public interface ITerminal {
    /// <summary>
    /// This will be called once per level entry.
    /// </summary>
    /// <param name="driver">The InputFieldDriver for the current level.</param>
    public void RegisterDriver(InputFieldDriver driver);
    
    /// <summary>
    /// Should return the InputFieldDriver for the current level.
    /// </summary>
    public InputFieldDriver GetDriver();

    /// <summary>
    /// Add a command to the Terminal.
    /// </summary>
    public void AddCommand(ICommand command);
    
    /// <summary>
    /// This should copy all commands to the given ITerminal.
    /// Usually that means calling AddCommand for every command that has been added via AddCommand.
    /// </summary>
    public void CopyCommandsTo(ITerminal terminal);
}