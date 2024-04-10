﻿namespace Computerdores.Vanillin.Commands; 

public class ViewThingCommand : ICommand {
    private readonly string _name;

    public ViewThingCommand(string name) {
        _name = name;
    }

    public string GetName() => _name;

    public CommandResult Execute(string input, ITerminal terminal) {
        Terminal vT = terminal.GetDriver().VanillaTerminal;
        TerminalNode n = Util.FindKeyword(terminal, "view").FindNoun(_name).result;
        vT.LoadTerminalImage(n);
        return new CommandResult(Util.TextPostProcess(vT, n), n.clearPreviousText);
    }

    public object Clone() => new ViewThingCommand(_name);
}