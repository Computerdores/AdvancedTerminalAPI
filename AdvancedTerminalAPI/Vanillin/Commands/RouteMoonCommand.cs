using System.Collections.Generic;
using System.Linq;

namespace Computerdores.Vanillin.Commands; 

public class RouteMoonCommand : ICommand, IAliasable {
    private readonly string _moonName;

    private bool _awaitingConfirmation;
    private CompatibleNoun _moon;
    
    public RouteMoonCommand(string moonName) {
        _moonName = moonName;
    }

    public string GetName() => _moonName;

    public CommandResult Execute(string input, ITerminal terminal) {
        Terminal vT = terminal.GetDriver().VanillaTerminal;
        TerminalNode n;
        if (!_awaitingConfirmation) {
            // get the vanilla keyword
            _moon = Util.FindKeyword(terminal, "route").FindNoun(_moonName);
            if (_moon.result.itemCost != 0 || _moon.result.buyRerouteToMoon == -2) {
                vT.totalCostOfItems = _moon.result.itemCost;
            } 
            // trigger the vanilla behaviour
            n = TerminalWrapper.Get(vT).LoadNode(_moon.result);
            // output
            _awaitingConfirmation = (n.terminalOptions?.Length ?? 0) > 0; // kinda janky, but isConfirmationNode is always false for these
            return new CommandResult(Util.TextPostProcess(vT, n), n.clearPreviousText, true, _awaitingConfirmation);
        }
        
        CompatibleNoun cn = _moon.result.FindTerminalOption(input);
        // if the input doesn't match any available option ignore it
        if (cn == null) return CommandResult.IGNORE_INPUT;
        
        n = TerminalWrapper.Get(vT).LoadNode(cn.result);
        return new CommandResult(Util.TextPostProcess(vT, n), n.clearPreviousText);
    }

    public ICommand CloneStateless() => new RouteMoonCommand(_moonName);
    
    public IEnumerable<ICommand> GetAll(ITerminal term) {
        return from noun in Util.FindKeyword(term, "route").compatibleNouns
            select new RouteMoonCommand(noun.noun.word);
    }

    public static RouteMoonCommand FromPlayerInput(Terminal term, string input) {
        return new RouteMoonCommand(Util.FindKeyword(term, "route").
            FindNoun(input).noun.word
        );
    }
}