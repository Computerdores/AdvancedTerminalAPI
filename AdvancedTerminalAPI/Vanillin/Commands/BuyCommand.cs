using System.Linq;
using HarmonyLib;

namespace Computerdores.Vanillin.Commands; 

public class BuyCommand : ICommand {
    private bool _awaitingConfirmation;

    private BuyItemCommand _command;
    
    public string GetName() => "buy";

    public CommandResult Execute(string input, ITerminal terminal) {
        if (_awaitingConfirmation) return _command.Execute(input, terminal);
        
        _awaitingConfirmation = true;
        string[] words = input.Split(' ');
        _command = BuyItemCommand.FromPlayerInput(terminal.GetDriver().VanillaTerminal, words.First());
        
        return _command == null ? CommandResult.IGNORE_INPUT : 
            _command.Execute(words.Skip(1).Join(delimiter: " "), terminal);
    }

    public object Clone() => new BuyCommand();
}