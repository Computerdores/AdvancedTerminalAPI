namespace Computerdores; 

public interface ITerminal {
    public delegate string ArgumentPredictor(string partialArguments);
    public delegate string DisplayTextSupplier(string finalArguments);
    
    public void RegisterDriver(InputFieldDriver driver);
    public InputFieldDriver GetDriver();

    public void AddCommand(ICommand command);
    public void CopyCommandsTo(ITerminal terminal);
}