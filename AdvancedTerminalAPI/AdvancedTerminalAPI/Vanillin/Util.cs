﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Computerdores.AdvancedTerminalAPI.Vanillin; 

public static class Util {

    public static TerminalNode GetSpecialNode(ITerminal terminal, int nodeIndex) => 
        GetSpecialNode(terminal.GetDriver().VanillaTerminal, nodeIndex);
    public static TerminalNode GetSpecialNode(Terminal vanillaTerminal, int nodeIndex) {
        return vanillaTerminal.terminalNodes.specialNodes[nodeIndex];
    }

    /// <summary>
    /// Match an IEnumerable against a string using the vanilla string matching.
    /// </summary>
    /// <param name="enumerable">The IEnumerable of objects to be matched.</param>
    /// <param name="word">The string to be matched against.</param>
    /// <param name="stringConverter">A delegate which, given an object of type <see cref="T"/>,
    ///     returns the string to be matched.</param>
    /// <param name="predicate">An additional condition that the result needs to satisfy (optional).</param>
    /// <param name="specificity"></param>
    /// <returns>An object of type <see cref="T"/> which matches the vanilla string matching rules,
    ///     when converted to a string using <paramref name="stringConverter" />.</returns>
    // ReSharper disable once MemberCanBePrivate.Global
    public static T VanillaStringMatch<T>(this IEnumerable<T> enumerable, string word,
        Converter<T, string> stringConverter, Predicate<T> predicate = null, int specificity = 3) {
        IEnumerable<T> array = enumerable as T[] ?? enumerable.ToArray();
        
        T tWord = array.FirstOrDefault(n =>
            string.Equals(stringConverter(n), word, StringComparison.CurrentCultureIgnoreCase) &&
            (predicate?.Invoke(n) ?? true)
        );
        
        if (tWord != null || word.Length < specificity) return tWord;

        for (int spec = word.Length; spec >= specificity && tWord == null; spec--) {
            tWord = array.FirstOrDefault(n =>
                stringConverter(n).StartsWith(word[..spec], StringComparison.CurrentCultureIgnoreCase) &&
                (predicate?.Invoke(n) ?? true)
            );
        }
        
        return tWord;
    }

    /// <summary>
    /// Find a terminal option using the vanilla string matching.
    /// </summary>
    public static CompatibleNoun FindTerminalOption(this TerminalNode node, string word, Predicate<CompatibleNoun> predicate = null)
        => node.terminalOptions.VanillaStringMatch(word, compatibleNoun => compatibleNoun.noun.word, predicate, 1);

    /// <summary>
    /// Find a compatible noun using the vanilla string matching.
    /// </summary>
    public static CompatibleNoun FindNoun(this TerminalKeyword keyword, string word, Predicate<CompatibleNoun> predicate = null)
        => keyword.compatibleNouns.VanillaStringMatch(word, compatibleNoun => compatibleNoun.noun.word, predicate);


    /// <summary>
    /// Find a keyword using the vanilla string matching.
    /// </summary>
    /// <param name="vanillaTerm">The Terminal instance.</param>
    /// <param name="word">The string, that was entered by the player, which the Keyword should match.</param>
    /// <param name="predicate">A predicate which evaluates additional conditions (optional).</param>
    public static TerminalKeyword FindKeyword(Terminal vanillaTerm, string word, Predicate<TerminalKeyword> predicate = null)
        => vanillaTerm.terminalNodes.allKeywords.VanillaStringMatch(word, keyword => keyword.word, predicate);

    /// <summary>
    /// Wrapper for <see cref="FindKeyword(Terminal,string,System.Predicate{TerminalKeyword})"/>.
    /// </summary>
    public static TerminalKeyword FindKeyword(ITerminal terminal, string word, Predicate<TerminalKeyword> predicate = null)
        => FindKeyword(terminal.GetDriver().VanillaTerminal, word, predicate);

    public static TerminalNode FindByKeyword(Terminal vanillaTerm, string word, Predicate<TerminalKeyword> predicate = null) {
        TerminalKeyword tWord = FindKeyword(vanillaTerm, word, predicate);
        return tWord != null ? tWord.specialKeywordResult : null;
    }

    public static string TextPostProcess(this TerminalNode node, Terminal vanillaTerm)
        => vanillaTerm.TextPostProcess(node.displayText, node);

    public static int GetPlayerIndexByName(string name, int specificity = 3) {
        TransformAndName a = StartOfRound.Instance.mapScreen.radarTargets.
            VanillaStringMatch(name, input => input.name, specificity: specificity);
        if (a == null) return -1;
        return StartOfRound.Instance.mapScreen.radarTargets.IndexOf(a);
    }

    public static string PredictMoonName(Terminal terminal, string partialInput)
        => terminal == null ? partialInput : FindKeyword(terminal, "route").FindNoun(partialInput).noun.word;

    public static string PredictConfirmation(string partialInput)
        => new List<string> { "CONFIRM", "DENY" }.VanillaStringMatch(partialInput, s => s, specificity: 1);

    public static string PredictPlayerName(string partialInput) { // untested
        int index = GetPlayerIndexByName(partialInput);
        return index != -1 ? StartOfRound.Instance.mapScreen.radarTargets[index].name : partialInput;
    }

    // ReSharper disable once Unity.NoNullPropagation
    public static string PredictBuyable(Terminal terminal, string partialInput)
        => FindKeyword(terminal, "buy").compatibleNouns.
            VanillaStringMatch(
                partialInput,
                cn => cn.noun.word,
                cn => cn.result.shipUnlockableID != -1 || cn.result.buyItemIndex != -1
            )?.noun?.word;
}