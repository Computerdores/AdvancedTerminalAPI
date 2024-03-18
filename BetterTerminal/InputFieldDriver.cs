using System;
using TMPro;

namespace Computerdores; 

public class InputFieldDriver {

    private const int MaxInputLength = 40;

    private readonly TMP_InputField _inputField;

    private string _displayedText = "\n\n\n";

    public string Input { get; private set; } = "";

    public InputFieldDriver(Terminal __instance) {
        // Init variables
        _inputField = __instance.screenText;
        // Add event listeners
        _inputField.onValueChanged.AddListener(OnInputFieldChanged);
        _inputField.onSubmit.AddListener(OnInputFieldSubmit);
    }
    
    public void DisplayText(string text, bool clearInput) {
        _displayedText = "\n\n\n"+text;
        if (clearInput) Input = "";
        _renderToInputField();
    }

    private void _renderToInputField() {
        _inputField.text = _displayedText + Input;
        _inputField.caretPosition = _inputField.text.Length;
    }
    
    
    
    
    // <---- Event handling ----> //
    
    public void OnInputFieldChanged(string newText) {
        if (newText.Length < _displayedText.Length) {
            Input = "";
        } else {
            Input = newText.Substring(_displayedText.Length, Math.Min(MaxInputLength, newText.Length - _displayedText.Length));
        }
        _renderToInputField();
    }

    public void OnInputFieldSubmit(string text) {
        // for testing
        _displayedText = text + "\n";
        Input = "";
        _renderToInputField();
        // TODO
        _inputField.ActivateInputField();
        _inputField.Select();
    }
    
    public void OnBeginUsingTerminal(bool firstTime) {
        // TODO
    }
}