﻿namespace Computerdores.AdvancedTerminalAPI.Vanillin.Commands; 

public class InfoThingCommand : ICommand {
    private readonly string _name;
    
    public InfoThingCommand(string name) {
        _name = name;
    }

    public string GetName() => _name;

    public CommandResult Execute(string input, ITerminal terminal) {
        Terminal vT = terminal.GetDriver().VanillaTerminal;
        TerminalNode n = Util.FindKeyword(vT, "info").FindNoun(_name).result;
        if (n.creatureFileID != -1) n = TerminalWrapper.Get(vT).LoadNode(n);
        return n == null ? CommandResult.GenericError : 
            new CommandResult(n.TextPostProcess(vT), n.clearPreviousText);
    }

    public ICommand CloneStateless() => new InfoThingCommand(_name);
}