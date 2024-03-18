using System;

namespace Computerdores; 

public class VanillinTerminal : ITerminal {

    private InputFieldDriver _driver;
    
    private Terminal VanillaTerminal => _driver.VanillaTerminal;
    private string Input => _driver.Input;
    
    public void RegisterDriver(InputFieldDriver driver) {
        _driver = driver;
        _driver.OnSubmit += OnSubmit;
        _driver.OnEnterTerminal += OnEnterTerminal;
    }

    public void PreAwake() { }
    public void PostAwake() { }
    public void PreStart() { }
    public void PostStart() { }
    public void PreUpdate() { }
    public void PostUpdate() { }
    public void AddCommand(ICommand command) {
        // TODO
    }

    private void OnSubmit(string text) {
        // TODO
    }
    
    private void OnEnterTerminal(bool firstTime) {
        // TODO
    }
}