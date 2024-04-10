using System.Collections.Generic;
using BepInEx;
using BepInEx.Logging;
using Computerdores.patch;
using Computerdores.Vanillin;
using HarmonyLib;

namespace Computerdores;

[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
public class AdvancedTerminalAPI : BaseUnityPlugin {
    private static AdvancedTerminalAPI Instance { get; set; }
    internal static ManualLogSource Log => Instance.Logger;
    private readonly Harmony _harmony = new(PluginInfo.PLUGIN_GUID);

    private static Method<ITerminal, InputFieldDriver> _terminalConstructor;
    private static List<ICommand> _commands = new();

    private static InputFieldDriver Driver { get; set; }
    private static ITerminal CustomTerminal { get; set; }

    public AdvancedTerminalAPI() {
        Instance = this;
        ReplaceITerminal(d => new VanillinTerminal(d));
    }

    // ReSharper disable once MemberCanBePrivate.Global
    public static void ReplaceITerminal(Method<ITerminal, InputFieldDriver> terminalConstructor) {
        _terminalConstructor = terminalConstructor;
        ReplaceITerminalInstance();
    }

    // ReSharper disable once UnusedMember.Global
    public static void AddCommand(ICommand command) {
        _commands.Add(command);
        CustomTerminal.AddCommand(command);
    }

    internal static void RegisterTerminal(Terminal terminal) {
        Driver = new InputFieldDriver(terminal);
        ReplaceITerminalInstance();
    }

    private static void ReplaceITerminalInstance() {
        CustomTerminal = _terminalConstructor(Driver);
        foreach (ICommand command in _commands) {
            CustomTerminal.AddCommand(command);
        }
    }

    private void Awake() {
        ApplyPatches();
    }

    private void ApplyPatches() {
        _harmony.PatchAll(typeof(TerminalPatch));
    }
}