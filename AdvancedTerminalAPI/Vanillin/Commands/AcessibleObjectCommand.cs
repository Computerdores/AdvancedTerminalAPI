﻿namespace Computerdores.Vanillin.Commands; 

public class AccessibleObjectCommand : ICommand {
    private readonly string _name;

    public AccessibleObjectCommand(string name) {
        _name = name;
    }
    
    public string GetName() => _name;

    /// <summary>
    /// For the vanilla implementation, see: <see cref="Terminal.ParsePlayerSentence"/>.
    /// </summary>
    public CommandResult Execute(string input, ITerminal terminal) {
        Terminal vT = terminal.GetDriver().VanillaTerminal;
        vT.CallFunctionInAccessibleTerminalObject(_name);
        vT.PlayBroadcastCodeEffect();
        return new CommandResult(Util.GetSpecialNode(vT, 19).displayText);
    }

    public object Clone() => new AccessibleObjectCommand(_name);
}