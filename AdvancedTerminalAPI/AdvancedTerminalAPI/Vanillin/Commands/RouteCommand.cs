using System.Collections.Generic;
using System.Linq;
using HarmonyLib;

namespace Computerdores.AdvancedTerminalAPI.Vanillin.Commands; 

public class RouteCommand : ICommand, IPredictable, IAliasable {
    private RouteMoonCommand _command;

    private bool _awaitingConfirmation;
    
    public string GetName() => "route";

    public string PredictInput(string partialInput, ITerminal terminal)
        => _awaitingConfirmation
                ? Util.PredictConfirmation(partialInput)
                : Util.PredictMoonName(terminal.GetDriver().VanillaTerminal, partialInput);

    public CommandResult Execute(string input, ITerminal terminal) {
        if (_awaitingConfirmation) return _command.Execute(input, terminal);

        _awaitingConfirmation = true;
        string[] words = input.Split(' ');
        _command = RouteMoonCommand.FromPlayerInput(terminal.GetDriver().VanillaTerminal, words.First());
        
        return _command?.Execute(words.Skip(1).Join(delimiter: " "), terminal) ?? CommandResult.IGNORE_INPUT;
    }
    
    public IEnumerable<ICommand> GetAll(ITerminal term) {
        return from noun in Util.FindKeyword(term, "route").compatibleNouns
            select new RouteMoonCommand(noun.noun.word);
    }

    public ICommand CloneStateless() => new RouteCommand();
}