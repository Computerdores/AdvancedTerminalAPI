using System;
using Computerdores.patch;
using TMPro;

namespace Computerdores; 

public class InputFieldDriver {

    private const int MaxInputLength = 40;

    private readonly TMP_InputField _inputField;

    private string _displayedText = "\n\n\n";
    private string _input = "";


    // <---- Public API ----> //

    // ReSharper disable once MemberCanBePrivate.Global
    public string Input {
        get => _input;
        set {
            _input = value;
            _renderToInputField();
        }
    }
    
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
        Plugin.customTerminal.RegisterDriver(this);
    }

    public void DisplayText(string text, bool clearInput) {
        if (text is null) return;
        _displayedText = "\n\n\n"+text;
        if (clearInput) Input = "";
        _renderToInputField();
    }

    public string GetDisplayedText() => _displayedText;


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
            int newInputLength = Math.Min(MaxInputLength, newText.Length - _displayedText.Length);
            Input = newText.Substring(_displayedText.Length, newInputLength);
        }
        _renderToInputField();
    }

    private void OnInputFieldSubmitHandler(string text) {
        OnSubmit?.Invoke(Input);
        _inputField.ActivateInputField();
        _inputField.Select();
    }

    private void OnEnterTerminalHandler(bool firstTime) {
        DisplayText("", true); // Counteract an effect from the EnterTerminal Handling of Terminal
        OnEnterTerminal?.Invoke(firstTime);
    } 
}