﻿namespace Computerdores.AdvancedTerminalAPI.Vanillin.Commands; 

public class ScanCommand : ICommand, IDescribable {
    public string GetName() => "scan";

    /// <summary>
    /// This extracts and uses the string from the vanilla command.
    /// It also uses the vanilla string formatting, see: <see cref="Terminal.TextPostProcess"/>.
    /// </summary>
    public CommandResult Execute(string input, ITerminal terminal) {
        Terminal vT = terminal.GetDriver().VanillaTerminal;
        TerminalNode node = Util.FindByKeyword(terminal.GetDriver().VanillaTerminal, "scan");
        return new CommandResult(vT.TextPostProcess(node.displayText, node));
    }

    public ICommand CloneStateless() => new ScanCommand();

    public string GetUsage()
        => "SCAN";
    public string GetDescription()
        => "To scan for the number of items left on the current planet.";
}