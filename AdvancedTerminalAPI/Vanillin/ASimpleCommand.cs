namespace Computerdores.Vanillin; 

public abstract class ASimpleCommand {

    public CommandResult Execute(string input, ITerminal terminal, out bool wantsMoreInput) {
        wantsMoreInput = false;
        return Execute(input, terminal);
    }

    protected abstract CommandResult Execute(string input, ITerminal terminal);
}