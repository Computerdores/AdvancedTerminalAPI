namespace Computerdores.AdvancedTerminalAPI.Vanillin.Commands; 

public class PingCommand : ICommand, IPredictable, IDescribable {

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

    public ICommand CloneStateless() => new PingCommand();

    public string GetUsage()
        => "PING [Radar booster name]";
    public string GetDescription()
        => "To make a radar booster play a noise.";
}