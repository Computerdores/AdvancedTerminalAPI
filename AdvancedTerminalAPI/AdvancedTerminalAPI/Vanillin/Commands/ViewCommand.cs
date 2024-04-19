using System.Collections.Generic;
using System.Linq;
using HarmonyLib;

namespace Computerdores.AdvancedTerminalAPI.Vanillin.Commands; 

public class ViewCommand : ICommand, IAliasable, IDescribable, IPredictable {
    public string GetName() => "view";

    public string PredictInput(string partialInput, ITerminal terminal)
        => Util.FindKeyword(terminal, "view").compatibleNouns
            .VanillaStringMatch(partialInput,
                cn => cn.noun.word,
                cn => cn.noun.defaultVerb != null
            ).noun.word;

    /// <summary>
    /// For the vanilla implementation, see: <see cref="Terminal.LoadNewNode"/>.
    /// </summary>
    public CommandResult Execute(string input, ITerminal terminal) {
        string[] words = input.Split(' ');
        var cmd = new ViewThingCommand(words[0]);
        return cmd.Execute(words.Skip(1).Join(delimiter: " "), terminal);
    }

    public ICommand CloneStateless() => new ViewCommand();

    public IEnumerable<ICommand> GetAll(ITerminal term) {
        return from noun in Util.FindKeyword(term, "view").compatibleNouns 
            where noun.noun.defaultVerb != null
            select new ViewThingCommand(noun.noun.word);
    }

    public string GetUsage()
        => "VIEW MONITOR";
    public string GetDescription()
        => "To toggle on AND off the main monitor's map cam.";
}