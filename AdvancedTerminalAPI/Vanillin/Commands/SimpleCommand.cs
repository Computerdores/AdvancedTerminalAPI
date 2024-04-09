﻿namespace Computerdores.Vanillin.Commands; 

public class SimpleCommand : ICommand {
    private string _name;
    
    public SimpleCommand(string name) {
        _name = name;
    }

    public string GetName() => _name;

    public CommandResult Execute(string input, ITerminal terminal) {
        Terminal vT = terminal.GetDriver().VanillaTerminal;
        TerminalNode n = Util.FindKeyword(vT, _name).specialKeywordResult;
        return n == null ? CommandResult.GENERIC_ERROR : 
            new CommandResult(Util.TextPostProcess(vT, n), n.clearPreviousText);
    }

    public object Clone() => new SimpleCommand(_name);
}