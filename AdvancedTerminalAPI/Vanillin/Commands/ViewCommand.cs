﻿using System.Linq;

namespace Computerdores.Vanillin.Commands; 

public class ViewCommand : ICommand, IPredictable {
    public string GetName() => "view";

    public string PredictInput(string partialInput) {
        throw new System.NotImplementedException(); // TODO
    }

    /// <summary>
    /// For the vanilla implementation, see: <see cref="Terminal.LoadNewNode"/>.
    /// </summary>
    public CommandResult Execute(string input, ITerminal terminal) {
        TerminalNode node = Util.FindKeyword(terminal.GetDriver().VanillaTerminal, "view")
            .FindNoun(input.Split(' ').First())
            .result;
        if (node == null) return CommandResult.GENERIC_ERROR;
        terminal.GetDriver().VanillaTerminal.LoadTerminalImage(node);
        return new CommandResult(node.displayText, node.clearPreviousText);
    }

    public object Clone() => new ViewCommand();
}