﻿using System.Collections.Generic;
using System.Linq;
using HarmonyLib;

namespace Computerdores.Vanillin.Commands; 

public class InfoCommand : ICommand {
    public string GetName() => "info";

    public CommandResult Execute(string input, ITerminal terminal) {
        string[] words = input.Split(' '); 
        return new InfoThingCommand(words[0]).Execute(words.Skip(1).Join(delimiter: " "), terminal);
    }

    public object Clone() => new InfoCommand();

    public static IEnumerable<ICommand> GetAll(ITerminal term) {
        TerminalKeyword kw = Util.FindKeyword(term.GetDriver().VanillaTerminal, "info");
        return from a in kw.compatibleNouns
            where a.noun.defaultVerb == kw
            select new InfoThingCommand(a.noun.word);
    }
}