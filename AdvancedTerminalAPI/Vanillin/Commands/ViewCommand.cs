using System.Collections.Generic;
using System.Linq;
using HarmonyLib;

namespace Computerdores.Vanillin.Commands; 

public class ViewCommand : ICommand, IAliasable {
    public string GetName() => "view";

    /// <summary>
    /// For the vanilla implementation, see: <see cref="Terminal.LoadNewNode"/>.
    /// </summary>
    public CommandResult Execute(string input, ITerminal terminal) {
        string[] words = input.Split(' ');
        var cmd = new ViewThingCommand(words[0]);
        return cmd.Execute(words.Skip(1).Join(delimiter: " "), terminal);
    }

    public object Clone() => new ViewCommand();

    public IEnumerable<ICommand> GetAll(ITerminal term) {
        return from noun in Util.FindKeyword(term, "view").compatibleNouns 
            where noun.noun.defaultVerb != null
            select new ViewThingCommand(noun.noun.word);
    }
}