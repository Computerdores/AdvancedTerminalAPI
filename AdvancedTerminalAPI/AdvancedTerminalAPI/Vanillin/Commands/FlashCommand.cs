namespace Computerdores.AdvancedTerminalAPI.Vanillin.Commands; 

public class FlashCommand : ICommand, IPredictable {
    public string GetName() => "flash";

    public string PredictInput(string partialInput) => Util.PredictPlayerName(partialInput);

    /// <summary>
    /// For the vanilla implementation, see: <see cref="Terminal.ParsePlayerSentence"/>.
    /// </summary>
    public CommandResult Execute(string input, ITerminal terminal) {
        int index = Util.GetPlayerIndexByName(input);
        if (index == -1) {
            index = StartOfRound.Instance.mapScreen.targetTransformIndex;
            if (!StartOfRound.Instance.mapScreen.radarTargets[index].isNonPlayer) {
                return CommandResult.GENERIC_ERROR;
            }
        }
        StartOfRound.Instance.mapScreen.FlashRadarBooster(index);
        return new CommandResult(Util.GetSpecialNode(terminal, 23).displayText);
    }

    public ICommand CloneStateless() => new FlashCommand();
}