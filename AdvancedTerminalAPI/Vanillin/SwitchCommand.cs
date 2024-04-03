namespace Computerdores.Vanillin; 

public class SwitchCommand : ASimpleCommand, ICommand {
    public string GetName() => "switch";

    public string PredictArguments(string partialArgumentsText) { // untested
        int index = Util.GetPlayerIndexByName(partialArgumentsText);
        return index != -1 ? StartOfRound.Instance.mapScreen.radarTargets[index].name : partialArgumentsText;
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