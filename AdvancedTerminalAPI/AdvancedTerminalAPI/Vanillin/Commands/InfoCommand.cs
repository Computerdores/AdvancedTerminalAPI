using System.Collections.Generic;
using System.Linq;
using HarmonyLib;

namespace Computerdores.AdvancedTerminalAPI.Vanillin.Commands; 

public class InfoCommand : ICommand, IAliasable, IPredictable {
    public string GetName() => "info";

    public string PredictInput(string partialInput, ITerminal terminal) {
        TerminalKeyword kw = Util.FindKeyword(terminal.GetDriver().VanillaTerminal, "info");
        if (kw.compatibleNouns.VanillaStringMatch(partialInput, cn => cn.noun.word)?.
                noun is { } a)
            return a.word;
        return partialInput;
    }

    public CommandResult Execute(string input, ITerminal terminal) {
        string[] words = input.Split(' '); 
        return new InfoThingCommand(words[0]).Execute(words.Skip(1).Join(delimiter: " "), terminal);
    }

    public ICommand CloneStateless() => new InfoCommand();

    public IEnumerable<ICommand> GetAll(ITerminal term) {
        TerminalKeyword kw = Util.FindKeyword(term.GetDriver().VanillaTerminal, "info");
        return from a in kw.compatibleNouns
            where a.noun.defaultVerb == kw
            select new InfoThingCommand(a.noun.word);
    }
}