using HarmonyLib;

namespace Computerdores.patch;

// Note: This whole thing will silently fail if there are ever more than 2 terminals in the level
[HarmonyPatch(typeof(Terminal))]
public class TerminalPatch {

    public static event InputFieldDriver.EnterTerminalEvent OnEnterTerminal;

    private static bool _usedTerminalThisSession;
    
    [HarmonyPrefix]
    [HarmonyPatch("Awake")]
    public static void AwakePrefix(Terminal __instance) {
        Plugin.driver = new InputFieldDriver(__instance);
    }

    [HarmonyPrefix]
    [HarmonyPatch("OnSubmit")]
    public static bool OnSubmitPrefix() {
        return false; // disable OnSubmit Event in Terminal class
    }
    
    [HarmonyPrefix]
    [HarmonyPatch("TextChanged")]
    public static bool TextChangedPrefix() {
        return false; // disable TextChanged Event in Terminal class
    }

    
    [HarmonyPrefix]
    [HarmonyPatch("BeginUsingTerminal")]
    public static void BeginUsingTerminalPrefix(Terminal __instance) {
        _usedTerminalThisSession = __instance.usedTerminalThisSession;
    }
    
    [HarmonyPostfix]
    [HarmonyPatch("BeginUsingTerminal")]
    public static void BeginUsingTerminalPostfix(Terminal __instance) {
        OnEnterTerminal?.Invoke(!_usedTerminalThisSession);
    }
}