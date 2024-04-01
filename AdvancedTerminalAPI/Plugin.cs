using BepInEx;
using BepInEx.Logging;
using Computerdores.Commands;
using Computerdores.patch;
using HarmonyLib;

namespace Computerdores;

[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
public class Plugin : BaseUnityPlugin {
    public static Plugin Instance { get; set; }
    public static ManualLogSource Log => Instance.Logger;
    private readonly Harmony _harmony = new(PluginInfo.PLUGIN_GUID);

    public static InputFieldDriver driver;
    public static ITerminal customTerminal = new VanillinTerminal();
    
    public Plugin() {
        Instance = this;
        customTerminal.AddCommand(new WelcomeCommand());
        customTerminal.AddCommand(new SwitchCommand());
        customTerminal.AddCommand(new ViewCommand());
    }

    public void ReplaceTerminal(ITerminal newTerminal) {
        ITerminal oldTerminal = customTerminal;
        customTerminal = newTerminal;
        oldTerminal.CopyCommandsTo(customTerminal);
    }

    private void Awake() {
        Log.LogInfo("Applying Patches...");
        ApplyPatches();
        Log.LogInfo("Patches applied.");
    }

    private void ApplyPatches() {
        _harmony.PatchAll(typeof(TerminalPatch));
    }
}