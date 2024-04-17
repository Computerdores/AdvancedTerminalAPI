using System.Collections.Generic;
using System.Linq;

namespace Computerdores.AdvancedTerminalAPI.Vanillin.Commands; 

public class SimpleCommand : ICommand {
    private string _name;
    
    public SimpleCommand(string name) {
        _name = name;
    }

    public string GetName() => _name;

    public CommandResult Execute(string input, ITerminal terminal) {
        Terminal vT = terminal.GetDriver().VanillaTerminal;
        TerminalNode n = Util.FindKeyword(vT, _name).specialKeywordResult;
        return n == null ? CommandResult.GenericError : 
            new CommandResult(n.TextPostProcess(vT), n.clearPreviousText);
    }

    public ICommand CloneStateless() => new SimpleCommand(_name);

    public static IEnumerable<SimpleCommand> GetAll() {
        return new[] { "help", "moons", "store", "upgrades", "decor", "storage", "sigurd", "bestiary" }.
            Select(s => new SimpleCommand(s));
    }
}