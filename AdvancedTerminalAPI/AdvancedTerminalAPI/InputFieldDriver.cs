using System;
using BepInEx;
using JetBrains.Annotations;
using TMPro;

namespace Computerdores.AdvancedTerminalAPI; 

public class InputFieldDriver {

    private int _maxInputLength = 40;

    private readonly TMP_InputField _inputField;

    private string _displayedText = "\n\n\n";
    private string _input = "";


    // <---- Public API ----> //

    /// <summary>
    /// The maximum number of characters the player is able to input.
    /// Decreasing it will automatically trim the users input if necessary.
    /// </summary>
    public int MaxInputLength {
        get => _maxInputLength;
        set {
            _maxInputLength = value;
            if (_input.Length > _maxInputLength)
                OnInputFieldChangedHandler(_inputField.text);
        }
    }

    /// <summary>
    /// Can be used to get or set the current Input by the Player.
    /// (Set is shown in the terminal)
    /// </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    public string Input {
        get => _input;
        set {
            _input = value;
            _renderToInputField();
        }
    }
    
    /// <summary>
    /// The Terminal being driven by this Driver
    /// </summary>
    public Terminal VanillaTerminal { get; }

    /// <summary>
    /// Triggered whenever the player enters the Terminal
    /// </summary>
    [Obsolete("Use TerminalWrapper.EnterTerminal instead.")]
    public event Consumer<bool> OnEnterTerminal;
    
    /// <summary>
    /// Triggered whenever the Player Submits what they typed in the Terminal
    /// </summary>
    public event Consumer<string> OnSubmit;
    
    /// <summary>
    /// Triggered whenever the Player types in the Terminal
    /// </summary>
    // ReSharper disable once EventNeverSubscribedTo.Global
    public event Consumer<string> OnInputChange;

    public InputFieldDriver(Terminal __instance) {
        // Init variables
        VanillaTerminal = __instance;
        _inputField = __instance.screenText;
        // Add event listeners
        _inputField.onValueChanged.AddListener(OnInputFieldChangedHandler);
        _inputField.onSubmit.AddListener(OnInputFieldSubmitHandler);
        TerminalWrapper.Get(__instance).EnterTerminal += OnEnterTerminalHandler;
    }
    
    /// <summary>
    /// Change the Text that is displayed in the console.
    /// </summary>
    /// <param name="text">The new text to be displayed. If text is null or whitespace,
    ///     this parameter won't affect the displayed text, but the Input will be reset.</param>
    /// <param name="clearScreen">Whether the text should added after or instead of the current text.</param>
    public void DisplayText([CanBeNull] string text, bool clearScreen) {
        if (clearScreen) {
            _displayedText = "\n";
        }
        if (!text.IsNullOrWhiteSpace()) {
            if (!clearScreen) _displayedText += Input;
            _displayedText += $"\n\n{text}";
        }
        Input = "";
        _renderToInputField();
    }

    /// <summary>
    /// Get the text last displayed using <see cref="DisplayText"/>.
    /// </summary>
    /// <returns>The displayed text without the Input.</returns>
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
            int newInputLength = Math.Min(_maxInputLength, newText.Length - _displayedText.Length);
            Input = newText.Substring(_displayedText.Length, newInputLength);
        }
        _renderToInputField();
        OnInputChange?.Invoke(Input);
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