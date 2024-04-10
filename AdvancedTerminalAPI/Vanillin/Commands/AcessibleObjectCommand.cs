using System.Collections.Generic;
using System.Linq;

namespace Computerdores.Vanillin.Commands; 

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

    public ICommand CloneStateless() => new AccessibleObjectCommand(_name);

    public static IEnumerable<AccessibleObjectCommand> GetAll(InputFieldDriver driver) =>
        driver.VanillaTerminal.terminalNodes.allKeywords.
            Where(keyword => keyword.accessTerminalObjects).
            Select(keyword => new AccessibleObjectCommand(keyword.word));
}