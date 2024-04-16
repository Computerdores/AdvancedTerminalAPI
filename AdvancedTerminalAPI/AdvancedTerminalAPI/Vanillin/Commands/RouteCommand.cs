using System.Linq;
using HarmonyLib;

namespace Computerdores.AdvancedTerminalAPI.Vanillin.Commands; 

public class RouteCommand : ICommand, IPredictable {
    private RouteMoonCommand _command;

    private bool _awaitingConfirmation;

    private readonly Terminal _vT;
    
    public RouteCommand(Terminal term) {
        _vT = term;
    }
    
    public string GetName() => "route";

    public string PredictInput(string partialInput)
        => _awaitingConfirmation ? Util.PredictConfirmation(partialInput) : Util.PredictMoonName(_vT, partialInput);

    public CommandResult Execute(string input, ITerminal terminal) {
        if (_awaitingConfirmation) return _command.Execute(input, terminal);

        _awaitingConfirmation = true;
        string[] words = input.Split(' ');
        _command = RouteMoonCommand.FromPlayerInput(terminal.GetDriver().VanillaTerminal, words.First());
        
        return _command?.Execute(words.Skip(1).Join(delimiter: " "), terminal) ?? CommandResult.IGNORE_INPUT;
    }

    public ICommand CloneStateless() => new RouteCommand(_vT);
}