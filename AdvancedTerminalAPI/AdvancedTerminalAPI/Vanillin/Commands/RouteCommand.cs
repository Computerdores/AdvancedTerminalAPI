﻿using System.Linq;
using HarmonyLib;

namespace Computerdores.AdvancedTerminalAPI.Vanillin.Commands; 

public class RouteCommand : ICommand {
    private RouteMoonCommand _command;

    private bool _awaitingConfirmation; 
    
    public string GetName() => "route";

    public CommandResult Execute(string input, ITerminal terminal) {
        if (_awaitingConfirmation) return _command.Execute(input, terminal);

        _awaitingConfirmation = true;
        string[] words = input.Split(' ');
        _command = RouteMoonCommand.FromPlayerInput(terminal.GetDriver().VanillaTerminal, words.First());
        
        return _command == null ? CommandResult.IGNORE_INPUT : 
            _command.Execute(words.Skip(1).Join(delimiter: " "), terminal);
    }

    public ICommand CloneStateless() => new RouteCommand();
}