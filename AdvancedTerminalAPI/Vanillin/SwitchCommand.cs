namespace Computerdores.Vanillin; 

public class SwitchCommand : ICommand {
    public string GetName() => "switch";

    public string PredictArguments(string partialArgumentsText) { // untested
        int index = Util.GetPlayerIndexByName(partialArgumentsText);
        return index != -1 ? StartOfRound.Instance.mapScreen.radarTargets[index].name : partialArgumentsText;
    }

    public (string output, bool clearScreen, bool success) Execute(string finalArgumentsText, ITerminal terminal) {
        int index = Util.GetPlayerIndexByName(finalArgumentsText);
        if (index == -1 && finalArgumentsText != "") return ("", false, false);
        if (index != -1) {
            StartOfRound.Instance.mapScreen.SwitchRadarTargetAndSync(index);
        } else {
            StartOfRound.Instance.mapScreen.SwitchRadarTargetForward(true);
        }
        return (Util.FindNoun(terminal.GetDriver().VanillaTerminal, "switch").specialKeywordResult.displayText, true, true);
    }
}