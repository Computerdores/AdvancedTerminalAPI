using System;

namespace Computerdores; 

public interface ICommand : ICloneable {
    /// <summary>
    /// This returns the string by which the command can be called.
    /// </summary>
    public string GetName();

    /// <summary>
    /// This processes input given to the Command.
    /// Terminals usually clone a Prototype Instance of the Command,
    /// and only call this on the clone.
    /// </summary>
    /// <param name="input">On the first Call this is usually the string of text entered by the player
    /// after the commands name. On subsequent calls this will be text directly entered by the Player in the Terminal,
    /// to be interpreted by the command instead of being interpreted by the Terminal.</param>
    /// <param name="terminal">The Terminal this Command is being executed in.
    /// Can be used to the current <see cref="InputFieldDriver"/> and <see cref="Terminal"/> instance. </param>
    /// <returns>A <see cref="CommandResult"/> object which gives details about the execution of the Command
    /// (e.g. whether it was successful).</returns>
    public CommandResult Execute(string input, ITerminal terminal);
}