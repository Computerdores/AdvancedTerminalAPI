using UnityEngine;

namespace Computerdores.AdvancedTerminalAPI.Vanillin.Commands; 

public class TransmitCommand : ICommand, IDescribable {

    public string GetName() => "transmit";

    /// <summary>
    /// For vanilla implementation see <see cref="Terminal.ParsePlayerSentence"/>.
    /// </summary>
    public CommandResult Execute(string input, ITerminal terminal) {
        Terminal vT = terminal.GetDriver().VanillaTerminal;
        var translator = Object.FindObjectOfType<SignalTranslator>();
        if (translator == null || !((double)Time.realtimeSinceStartup - translator.timeLastUsingSignalTranslator > 8.0) || string.IsNullOrEmpty(input))
            return CommandResult.GENERIC_ERROR;
        if (!vT.IsServer)
            translator.timeLastUsingSignalTranslator = Time.realtimeSinceStartup;
        HUDManager.Instance.UseSignalTranslatorServerRpc(input[..Mathf.Min(input.Length, 10)]);
        TerminalNode n = Util.GetSpecialNode(terminal, 22);
        return new CommandResult(n.displayText, n.clearPreviousText);
    }

    public ICommand CloneStateless() => new TransmitCommand();

    public string GetUsage()
        => "TRANSMIT [message]";
    public string GetDescription()
        => "To transmit a message with the signal translator.";
}