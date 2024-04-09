using System.Collections.Generic;
using System.Linq;
using HarmonyLib;

namespace Computerdores.Vanillin.Commands; 

public class ViewCommand : ICommand, IPredictable {
    public string GetName() => "view";

    public string PredictInput(string partialInput) {
        throw new System.NotImplementedException(); // TODO
    }

    /// <summary>
    /// For the vanilla implementation, see: <see cref="Terminal.LoadNewNode"/>.
    /// </summary>
    public CommandResult Execute(string input, ITerminal terminal) {
        ViewThingCommand cmd = FromPlayerInput(terminal.GetDriver().VanillaTerminal, input);
        return cmd?.Execute(input.Split(' ').Skip(1).Join(delimiter: " "), terminal) ?? CommandResult.GENERIC_ERROR;
    }

    public object Clone() => new ViewCommand();

    public static IEnumerable<ICommand> GetAll(ITerminal term) {
        return from noun in Util.FindKeyword(term, "view").compatibleNouns 
            where noun.noun.defaultVerb != null
            select new ViewThingCommand(noun.noun.word);
    }

    private static ViewThingCommand FromPlayerInput(Terminal term, string input) =>
        new(Util.FindKeyword(term, "view").FindNoun(input).noun.word);
}