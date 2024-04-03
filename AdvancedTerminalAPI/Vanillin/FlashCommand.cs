﻿namespace Computerdores.Vanillin; 

public class FlashCommand : SimpleCommand, ICommand, IPredictable {
    public string GetName() => "flash";

    public string PredictInput(string partialInput) => Util.PredictPlayerName(partialInput);

    protected override CommandResult Execute(string input, ITerminal terminal) {
        CommandResult result = new();
        int index = Util.GetPlayerIndexByName(input);
        if (index == -1) {
            index = StartOfRound.Instance.mapScreen.targetTransformIndex;
            if (!StartOfRound.Instance.mapScreen.radarTargets[index].isNonPlayer) {
                result.success = false;
                result.clearScreen = false;
                return result;
            }
        }
        StartOfRound.Instance.mapScreen.FlashRadarBooster(index);
        result.output = Util.GetSpecialNode(terminal.GetDriver().VanillaTerminal, 23).displayText;
        return result;
    }

    public object Clone() => new FlashCommand();
}