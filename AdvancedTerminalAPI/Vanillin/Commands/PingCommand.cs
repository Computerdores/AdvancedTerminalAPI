namespace Computerdores.Vanillin.Commands; 

public class PingCommand : ICommand, IPredictable {

    public string GetName() => "ping";

    public string PredictInput(string partialInput) => Util.PredictPlayerName(partialInput);
    
    /// <summary>
    /// For the vanilla implementation, see: <see cref="Terminal.ParsePlayerSentence"/>.
    /// </summary>
    public CommandResult Execute(string input, ITerminal terminal) {
        int index = Util.GetPlayerIndexByName(input);
        if (index == -1) return CommandResult.GENERIC_ERROR;
        StartOfRound.Instance.mapScreen.PingRadarBooster(index);
        return new CommandResult(Util.GetSpecialNode(terminal, 21).displayText);
    }

    public object Clone() => new PingCommand();

    
}