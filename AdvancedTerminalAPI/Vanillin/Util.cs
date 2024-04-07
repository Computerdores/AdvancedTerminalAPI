using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Computerdores.Vanillin; 

public static class Util {

    public static TerminalNode GetSpecialNode(ITerminal terminal, int nodeIndex) => 
        GetSpecialNode(terminal.GetDriver().VanillaTerminal, nodeIndex);
    public static TerminalNode GetSpecialNode(Terminal vanillaTerminal, int nodeIndex) {
        return vanillaTerminal.terminalNodes.specialNodes[nodeIndex];
    }

    public static TerminalKeyword FindKeyword(Terminal vanillaTerm, string word, Predicate<TerminalKeyword> predicate = null) {
        TerminalKeyword tWord = vanillaTerm.terminalNodes.allKeywords.
            FirstOrDefault(w => w.word == word && (predicate?.Invoke(w) ?? true));
        if (tWord == null && word.Length >= 3)
            tWord = vanillaTerm.terminalNodes.allKeywords.
                FirstOrDefault(w => w.word.StartsWith(word[..3]) & (predicate?.Invoke(w) ?? true));
        return tWord;
    }

    public static TerminalNode FindByKeyword(Terminal vanillaTerm, string word, Predicate<TerminalKeyword> predicate = null) {
        TerminalKeyword tWord = FindKeyword(vanillaTerm, word, predicate);
        return tWord != null ? tWord.specialKeywordResult : null;
    }
    
    public static TerminalNode FindNode(Terminal vanillaTerm, string verb, [CanBeNull] string noun) {
        // find fitting verb
        TerminalKeyword tVerb = vanillaTerm.terminalNodes.allKeywords.
            FirstOrDefault(v => v.isVerb && v.word == verb);
        if (tVerb == null && verb.Length >= 3) tVerb = vanillaTerm.terminalNodes.allKeywords.
            FirstOrDefault(v => v.isVerb && v.word.StartsWith(verb[..3]));
        Plugin.Log.LogDebug($"tVerb is: {tVerb}, '{tVerb?.word}', '{tVerb?.specialKeywordResult?.displayText}'");
        if (tVerb == null || noun == null) return tVerb != null ? tVerb.specialKeywordResult : null; // if no noun given, return node of verb
        // find fitting noun
        CompatibleNoun tNoun = tVerb.compatibleNouns?.FirstOrDefault(n => n.noun.word == noun);
        if (tNoun == null && noun.Length >= 3) tNoun = tVerb.compatibleNouns?.
            FirstOrDefault(n => n.noun.word.StartsWith(noun[..3]));
        return tNoun?.result;
    }
    
    public static int GetPlayerIndexByName(string name) {
        // Note: I didn't come up with this logic
        // I just reimplemented the base game name detection in a more efficient way (see: CheckForPlayerNameCommand)
        if (name.Length < 3) return -1;
        var playerNames = new List<string>(
            StartOfRound.Instance.mapScreen.radarTargets.Select(element => element.name.ToLower())
        );
        name = name.ToLower();
        for (var i = 0; i < playerNames.Count; i++) {
            if (playerNames[i] == name) return i;
        }
        for (var i = 0; i < playerNames.Count; i++) {
            if (playerNames[i].StartsWith(name[..3])) return i;
        }
        return -1;
    }

    public static string PredictConfirmation(string partialInput) {
        return partialInput.ToLower().StartsWith("c") ? "CONFIRM" : "DENY";
    }

    public static string PredictPlayerName(string partialInput) { // untested
        int index = GetPlayerIndexByName(partialInput);
        return index != -1 ? StartOfRound.Instance.mapScreen.radarTargets[index].name : partialInput;
    }
}