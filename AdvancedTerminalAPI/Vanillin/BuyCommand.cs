using System.Linq;
using HarmonyLib;

namespace Computerdores.Vanillin; 

public class BuyCommand : ICommand {
    private bool _awaitingConfirmation;

    private BuyItemCommand _command;
    
    public string GetName() => "buy";

    public CommandResult Execute(string input, ITerminal terminal, out bool wantsMoreInput) {
        Terminal vT = terminal.GetDriver().VanillaTerminal;

        if (_awaitingConfirmation) return _command.Execute(input, terminal, out wantsMoreInput);
        
        _awaitingConfirmation = true;
        string[] words = input.Split(' ');
        _command = BuyItemCommand.FromPlayerInput(terminal.GetDriver().VanillaTerminal, words.First());
        if (_command != null) return _command.Execute(words.Skip(1).Join(delimiter: " "), terminal, out wantsMoreInput);
        wantsMoreInput = true;
        return new CommandResult(null, false, true);
    }

    public object Clone() => new BuyCommand();
}