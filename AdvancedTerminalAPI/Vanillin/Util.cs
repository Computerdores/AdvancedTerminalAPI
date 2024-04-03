using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Computerdores.Vanillin; 

public static class Util {
    public static TerminalNode GetSpecialNode(Terminal vanillaTerminal, int nodeIndex) {
        return vanillaTerminal.terminalNodes.specialNodes[nodeIndex];
    }

    public static TerminalKeyword FindNoun(Terminal vanillaTerm, string verb) {
        TerminalKeyword tNoun = vanillaTerm.terminalNodes.allKeywords.
            FirstOrDefault(n => !n.isVerb && n.word == verb);
        if (tNoun == null && verb.Length >= 3) tNoun = vanillaTerm.terminalNodes.allKeywords.
            FirstOrDefault(n => !n.isVerb && n.word.StartsWith(verb[..3]));
        return tNoun;
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
}