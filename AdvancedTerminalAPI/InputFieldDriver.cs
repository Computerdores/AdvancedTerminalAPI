using System;
using Computerdores.patch;
using TMPro;

namespace Computerdores; 

public class InputFieldDriver {

    private const int MaxInputLength = 40;

    private readonly TMP_InputField _inputField;

    private string _displayedText = "\n\n\n";


    // <---- Public API ----> //

    public string Input { get; private set; } = "";
    
    public Terminal VanillaTerminal { get; }

    public delegate void EnterTerminalEvent(bool firstTime);
    public delegate void SubmitEvent(string text);

    public event EnterTerminalEvent OnEnterTerminal;
    public event SubmitEvent OnSubmit;

    public InputFieldDriver(Terminal __instance) {
        // Init variables
        VanillaTerminal = __instance;
        _inputField = __instance.screenText;
        // Add event listeners
        _inputField.onValueChanged.AddListener(OnInputFieldChangedHandler);
        _inputField.onSubmit.AddListener(OnInputFieldSubmitHandler);
        TerminalPatch.OnEnterTerminal += OnEnterTerminalHandler;
        // Register yourself with the Modded Terminal
        Plugin.CustomTerminal.RegisterDriver(this);
    }

    public void DisplayText(string text, bool clearInput) {
        _displayedText = "\n\n\n"+text;
        if (clearInput) Input = "";
        _renderToInputField();
    }


    // <---- Private Methods ----> //

    private void _renderToInputField() {
        _inputField.text = _displayedText + Input;
        _inputField.caretPosition = _inputField.text.Length;
    }


    // <---- Event handling ----> //

    private void OnInputFieldChangedHandler(string newText) {
        if (newText.Length < _displayedText.Length) {
            Input = "";
        } else {
            Input = newText.Substring(_displayedText.Length, Math.Min(MaxInputLength, newText.Length - _displayedText.Length));
        }
        _renderToInputField();
    }

    private void OnInputFieldSubmitHandler(string text) {
        OnSubmit?.Invoke(text);
        _inputField.ActivateInputField();
        _inputField.Select();
    }

    private void OnEnterTerminalHandler(bool firstTime) {
        DisplayText("", true); // Counteract an effect from the EnterTerminal Handling of Terminal
        OnEnterTerminal?.Invoke(firstTime);
    } 
}