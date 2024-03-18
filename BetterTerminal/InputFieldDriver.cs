using System;

namespace Computerdores; 

public class InputFieldDriver {

    private Terminal instance;
    
    public InputFieldDriver(Terminal __instance) {
        instance = __instance;
        instance.screenText.onValueChanged.AddListener(OnInputFieldChanged);
    }
    
    public void DisplayText(string text, bool clearInput) {
        throw new NotImplementedException();
    }

    public string GetInputText() {
        throw new NotImplementedException();
    }
    
    public void OnInputFieldChanged(string newText) {
        throw new NotImplementedException();
    }
}