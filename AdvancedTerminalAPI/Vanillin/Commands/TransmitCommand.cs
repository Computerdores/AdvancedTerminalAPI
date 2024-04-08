using UnityEngine;

namespace Computerdores.Vanillin.Commands; 

public class TransmitCommand : ICommand {

    public string GetName() => "transmit";

    /// <summary>
    /// For vanilla implementation see <see cref="Terminal.ParsePlayerSentence"/>.
    /// </summary>
    public CommandResult Execute(string input, ITerminal terminal) {
        Terminal vT = terminal.GetDriver().VanillaTerminal;
        var translator = Object.FindObjectOfType<SignalTranslator>();
        if (translator == null || !((double)Time.realtimeSinceStartup - translator.timeLastUsingSignalTranslator > 8.0) || string.IsNullOrEmpty(input))
            return new CommandResult { success = false };
        if (!vT.IsServer)
            translator.timeLastUsingSignalTranslator = Time.realtimeSinceStartup;
        HUDManager.Instance.UseSignalTranslatorServerRpc(input[..Mathf.Min(input.Length, 10)]);
        TerminalNode n = Util.GetSpecialNode(terminal, 22);
        return new CommandResult(n.displayText, n.clearPreviousText);
    }

    public object Clone() => new TransmitCommand();
}