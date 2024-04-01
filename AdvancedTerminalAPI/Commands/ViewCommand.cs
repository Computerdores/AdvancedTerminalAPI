using System.Linq;
using JetBrains.Annotations;

namespace Computerdores.Commands; 

public class ViewCommand : ICommand {
    public string GetName() => "view";

    public string PredictArguments(string partialArgumentsText) {
        throw new System.NotImplementedException(); // TODO
    }

    public (string output, bool clearScreen, bool success) Execute(string finalArgumentsText, ITerminal terminal) {
        TerminalNode node = FindNode(terminal.GetDriver().VanillaTerminal, "view",
            finalArgumentsText.Split(' ').First());
        if (node == null) return ("", false, false);
        terminal.GetDriver().VanillaTerminal.LoadTerminalImage(node);
        return (node.displayText, node.clearPreviousText, true);
    }

    private static TerminalNode FindNode(Terminal vanillaTerm, string verb, [CanBeNull] string noun) {
        // find fitting verb
        TerminalKeyword tVerb = vanillaTerm.terminalNodes.allKeywords.
            FirstOrDefault(v => v.isVerb && v.word == verb);
        if (tVerb == null && verb.Length >= 3) tVerb = vanillaTerm.terminalNodes.allKeywords.
            FirstOrDefault(v => v.isVerb && v.word.StartsWith(verb[..3]));
        if (tVerb == null || noun == null) return tVerb != null ? tVerb.specialKeywordResult : null; // if no noun given, return node of verb
        // find fitting noun
        CompatibleNoun tNoun = tVerb.compatibleNouns?.FirstOrDefault(n => n.noun.word == noun);
        if (tNoun == null && noun.Length >= 3) tNoun = tVerb.compatibleNouns?.
            FirstOrDefault(n => n.noun.word.StartsWith(noun[..3]));
        return tNoun?.result;
    }
}