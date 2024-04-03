using HarmonyLib;

namespace Computerdores.patch;

// Note: This whole thing will silently fail if there are ever more than 2 terminals in the level
[HarmonyPatch(typeof(Terminal))]
public class TerminalPatch {

    public delegate void SimpleEvent();

    public static event InputFieldDriver.EnterTerminalEvent OnEnterTerminal;

    public static event SimpleEvent PreAwake;
    public static event SimpleEvent PostAwake;
    public static event SimpleEvent PreStart;
    public static event SimpleEvent PostStart;
    public static event SimpleEvent PreUpdate;
    public static event SimpleEvent PostUpdate;

    private static bool _usedTerminalThisSession;
    
    [HarmonyPrefix]
    [HarmonyPatch("Awake")]
    public static void AwakePrefix(Terminal __instance) {
        Plugin.driver = new InputFieldDriver(__instance);
        PreAwake?.Invoke();
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
    
    [HarmonyPostfix]
    [HarmonyPatch("Awake")]
    public static void AwakePostfix() {
        PostAwake?.Invoke();
    }
    
    [HarmonyPrefix]
    [HarmonyPatch("Start")]
    public static void StartPrefix() {
        PreStart?.Invoke();
    }
    
    [HarmonyPostfix]
    [HarmonyPatch("Start")]
    public static void StartPostfix() {
        PostStart?.Invoke();
    }
    
    [HarmonyPrefix]
    [HarmonyPatch("Update")]
    public static void UpdatePrefix() {
        PreUpdate?.Invoke();
    }
    
    [HarmonyPostfix]
    [HarmonyPatch("Update")]
    public static void UpdatePostfix() {
        PostUpdate?.Invoke();
    }
}