namespace Computerdores.Vanillin; 

public class PingCommand : SimpleCommand, ICommand, IPredictable {

    public string GetName() => "ping";

    public string PredictInput(string partialInput) => Util.PredictPlayerName(partialInput);
    
    /// <summary>
    /// For the vanilla implementation, see: <see cref="Terminal.ParsePlayerSentence"/>.
    /// </summary>
    protected override CommandResult Execute(string input, ITerminal terminal) {
        int index = Util.GetPlayerIndexByName(input);
        CommandResult result = new();
        if (index != -1) {
            StartOfRound.Instance.mapScreen.PingRadarBooster(index);
            result.output = Util.GetSpecialNode(terminal, 21).displayText;
        } else {
            result.success = false;
            result.clearScreen = false;
        }
        return result;
    }

    public object Clone() => new PingCommand();

    
}