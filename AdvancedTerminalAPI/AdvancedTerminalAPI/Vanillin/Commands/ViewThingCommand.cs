﻿namespace Computerdores.AdvancedTerminalAPI.Vanillin.Commands; 

public class ViewThingCommand : ICommand {
    private readonly string _name;

    public ViewThingCommand(string name) {
        _name = name;
    }

    public string GetName() => _name;

    public CommandResult Execute(string input, ITerminal terminal) {
        Terminal vT = terminal.GetDriver().VanillaTerminal;
        TerminalNode n = Util.FindKeyword(terminal, "view").FindNoun(_name).result;
        n = TerminalWrapper.Get(vT).LoadNode(n);
        return new CommandResult(n.TextPostProcess(vT), n.clearPreviousText);
    }

    public ICommand CloneStateless() => new ViewThingCommand(_name);
}