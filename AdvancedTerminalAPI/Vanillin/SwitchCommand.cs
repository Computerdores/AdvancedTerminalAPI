namespace Computerdores.Vanillin; 

public class SwitchCommand : SimpleCommand, ICommand, IPredictable {
    public string GetName() => "switch";

    public string PredictInput(string partialInput) => Util.PredictPlayerName(partialInput);

    /// <summary>
    /// Very similar to the vanilla implementation, see:
    /// <see cref="Terminal.ParsePlayerSentence"/> and <see cref="Terminal.RunTerminalEvents"/>.
    /// </summary>
    protected override CommandResult Execute(string input, ITerminal terminal) {
        int index = Util.GetPlayerIndexByName(input);
        if (index == -1 && input != "") return new CommandResult("", false, false);
        if (index != -1) {
            StartOfRound.Instance.mapScreen.SwitchRadarTargetAndSync(index);
        } else {
            StartOfRound.Instance.mapScreen.SwitchRadarTargetForward(true);
        }
        return new CommandResult(
            Util.FindByKeyword(terminal.GetDriver().VanillaTerminal, "switch").displayText,
            true, true
        );
    }

    public object Clone() => new SwitchCommand();
}