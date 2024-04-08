using System.Linq;
using HarmonyLib;

namespace Computerdores.Vanillin.Commands; 

public class BuyCommand : ICommand {
    private bool _awaitingConfirmation;

    private BuyItemCommand _command;
    
    public string GetName() => "buy";

    public CommandResult Execute(string input, ITerminal terminal) {
        Terminal vT = terminal.GetDriver().VanillaTerminal;

        if (_awaitingConfirmation) return _command.Execute(input, terminal);
        
        _awaitingConfirmation = true;
        string[] words = input.Split(' ');
        _command = BuyItemCommand.FromPlayerInput(terminal.GetDriver().VanillaTerminal, words.First());
        
        if (_command == null) return new CommandResult(null, false, true, true);
        return _command.Execute(words.Skip(1).Join(delimiter: " "), terminal);
    }

    public object Clone() => new BuyCommand();
}