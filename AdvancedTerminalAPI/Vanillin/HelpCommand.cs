namespace Computerdores.Vanillin; 

public class HelpCommand : ASimpleCommand, ICommand {
    public string GetName() => "help";

    protected override CommandResult Execute(string input, ITerminal terminal) {
        Terminal vT = terminal.GetDriver().VanillaTerminal;
        return new CommandResult(Util.GetSpecialNode(vT, 13).displayText.
            Replace("[numberOfItemsOnRoute]",
                vT.numberOfItemsInDropship > 0
                    ? $"{vT.numberOfItemsInDropship} purchased items on route."
                    : ""
                ),
            true, true);
    }

    public object Clone() => new HelpCommand();
}