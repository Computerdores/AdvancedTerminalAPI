using BepInEx;

namespace Computerdores.AdvancedTerminalAPI.Vanillin.Commands; 

public class SwitchCommand : ICommand, IPredictable, IDescribable {
    public string GetName() => "switch";

    public string PredictInput(string partialInput, ITerminal terminal) => Util.PredictPlayerName(partialInput);

    /// <summary>
    /// Very similar to the vanilla implementation, see:
    /// <see cref="Terminal.ParsePlayerSentence"/> and <see cref="Terminal.RunTerminalEvents"/>.
    /// </summary>
    public CommandResult Execute(string input, ITerminal terminal) {
        int index = Util.GetPlayerIndexByName(input);
        if (index == -1 && input.IsNullOrWhiteSpace()) return CommandResult.GenericError;
        if (index != -1) {
            StartOfRound.Instance.mapScreen.SwitchRadarTargetAndSync(index);
        } else {
            StartOfRound.Instance.mapScreen.SwitchRadarTargetForward(true);
        }
        return new CommandResult(Util.FindByKeyword(terminal.GetDriver().VanillaTerminal, "switch").displayText);
    }

    public ICommand CloneStateless() => new SwitchCommand();

    public string GetUsage()
        => "SWITCH [Player name]";
    public string GetDescription()
        => "To switch view to a player on the main monitor.";
}