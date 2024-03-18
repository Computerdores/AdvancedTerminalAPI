using BepInEx;
using BepInEx.Logging;
using Computerdores.patch;
using HarmonyLib;

namespace Computerdores;

[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
public class Plugin : BaseUnityPlugin {
    public static Plugin Instance { get; set; }
    public static ManualLogSource Log => Instance.Logger;
    private readonly Harmony _harmony = new(PluginInfo.PLUGIN_GUID);

    public static InputFieldDriver Driver;
    public static ITerminal CustomTerminal = new VanillinTerminal();

    public Plugin() {
        Instance = this;
        CustomTerminal.AddCommand(new WelcomeCommand());
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