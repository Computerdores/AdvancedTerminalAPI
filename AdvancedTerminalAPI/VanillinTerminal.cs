﻿using System;
using System.Collections.Generic;
using System.Linq;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine.Yoga;

namespace Computerdores;

public class VanillinTerminal : ITerminal {
    private InputFieldDriver _driver;

    private readonly Dictionary<string, ICommand> _commands = new();

    private static ManualLogSource Log => Plugin.Log;

    public void RegisterDriver(InputFieldDriver driver) {
        _driver = driver;
        _driver.OnSubmit += OnSubmit;
        _driver.OnEnterTerminal += OnEnterTerminal;
    }

    public InputFieldDriver GetDriver() => _driver;

    public void PreAwake() { }
    public void PostAwake() { }
    public void PreStart() { }
    public void PostStart() { }
    public void PreUpdate() { }
    public void PostUpdate() { }

    public void AddCommand(ICommand command) {
        _commands[command.GetName()] = command;
    }
    public void CopyCommandsTo(ITerminal terminal) {
        foreach (ICommand command in _commands.Values) {
            terminal.AddCommand(command);
        }
    }

    private void OnSubmit(string text) {
        string[] words = text.Split(' ');
        string arguments = words.Length == 1 ? "" : words.Skip(1).Join(delimiter: " ");
        ICommand nCommand = FindCommand(words[0]);
        if (nCommand is {} command) {
            Log.LogInfo($"Found Command for word: '{words[0]}'");
            (string outp, bool clear, bool success) = command.Execute(arguments, this);
            if (success) {
                _driver.DisplayText(outp, clear);
            } else {
                Log.LogInfo($"Command execution failed for input: '{text}'");
                _driver.DisplayText("[There was no object supplied with the action, or your word was typed incorrectly or does not exist.]\n\n", true);
            }
        } else {
            Log.LogInfo($"Did not find Command for input: '{text}'");
            _driver.DisplayText("[There was no action supplied with the word.]\n\n", true);
        }
    }

    private void OnEnterTerminal(bool firstTime) {
        ICommand welcomeCommand = firstTime ? FindCommand("welcome") : FindCommand("help");
        Log.LogInfo("Entering Terminal"+(firstTime ? " for the first time" : "")+".");
        _driver.DisplayText(welcomeCommand?.Execute("", this).output, true);
    }
    

    private ICommand FindCommand(string command) {
        string[] keys = _commands.Keys.Where(cmd => cmd.StartsWith(command)).ToArray();
        return keys.Length != 1 ? null : _commands[keys[0]];
    }
}