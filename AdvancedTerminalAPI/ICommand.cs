using System;

namespace Computerdores; 

public interface ICommand : ICloneable {
    public string GetName();

    public string PredictArguments(string partialArgumentsText);

    public CommandResult Execute(string input, ITerminal terminal, out bool wantsMoreInput);
}