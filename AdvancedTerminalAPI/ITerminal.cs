namespace Computerdores; 

public interface ITerminal {
    public delegate string ArgumentPredictor(string partialArguments);
    public delegate string DisplayTextSupplier(string finalArguments);
    
    public void RegisterDriver(InputFieldDriver driver);
    
    public void PreAwake();
    public void PostAwake();
    public void PreStart();
    public void PostStart();
    public void PreUpdate();
    public void PostUpdate();

    public void AddCommand(ICommand command);
}