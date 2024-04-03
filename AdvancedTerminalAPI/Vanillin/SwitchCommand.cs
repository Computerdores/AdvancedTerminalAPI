namespace Computerdores.Vanillin; 

public class SwitchCommand : SimpleCommand, ICommand, IPredictable {
    public string GetName() => "switch";

    public string PredictInput(string partialInput) { // untested
        int index = Util.GetPlayerIndexByName(partialInput);
        return index != -1 ? StartOfRound.Instance.mapScreen.radarTargets[index].name : partialInput;
    }

    protected override CommandResult Execute(string input, ITerminal terminal) {
        int index = Util.GetPlayerIndexByName(input);
        if (index == -1 && input != "") return new CommandResult("", false, false);
        if (index != -1) {
            StartOfRound.Instance.mapScreen.SwitchRadarTargetAndSync(index);
        } else {
            StartOfRound.Instance.mapScreen.SwitchRadarTargetForward(true);
        }
        return new CommandResult(
            Util.FindNoun(terminal.GetDriver().VanillaTerminal, "switch").specialKeywordResult.displayText,
            true, true
        );
    }

    public object Clone() => new SwitchCommand();
}