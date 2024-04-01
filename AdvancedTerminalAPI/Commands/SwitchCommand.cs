using System.Collections.Generic;
using System.Linq;

namespace Computerdores.Commands; 

public class SwitchCommand : ICommand {
    public string GetName() {
        return "switch";
    }

    public string PredictArguments(string partialArgumentsText) { // untested
        int index = GetPlayerIndexByName(partialArgumentsText);
        return index != -1 ? StartOfRound.Instance.mapScreen.radarTargets[index].name : partialArgumentsText;
    }

    public (string output, bool clearScreen, bool success) Execute(string finalArgumentsText, ITerminal terminal) {
        int index = GetPlayerIndexByName(finalArgumentsText); 
        if (index != -1 || finalArgumentsText == "") {
            if (index != -1) {
                StartOfRound.Instance.mapScreen.SwitchRadarTargetAndSync(index);
            } else {
                StartOfRound.Instance.mapScreen.SwitchRadarTargetForward(true);
            }
            return ("Switching Radar cam view.\n\n", true, true);
        }
        return ("", false, false);
    }

    private static int GetPlayerIndexByName(string name) {
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