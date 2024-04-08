using HarmonyLib;

namespace Computerdores.patch;

// Note: This whole thing will silently fail if there are ever more than 2 terminals in the level
[HarmonyPatch(typeof(Terminal))]
public static class TerminalPatch {

    public delegate void SimpleEvent();

    public static event InputFieldDriver.EnterTerminalEvent OnEnterTerminal;

    public static event SimpleEvent PreAwake;
    public static event SimpleEvent PostAwake;
    public static event SimpleEvent PreStart;
    public static event SimpleEvent PostStart;
    public static event SimpleEvent PreUpdate;
    public static event SimpleEvent PostUpdate;
    
    private static bool _redirectLoadNewNode;

    private static bool _usedTerminalThisSession;
    
    public static TerminalNode lastLoadedNode;

    [HarmonyPrefix]
    [HarmonyPatch(nameof(Terminal.LoadNewNode))]
    private static bool InterceptLoadNewNode(this Terminal __instance, TerminalNode node) {
        if (!_redirectLoadNewNode) return true;
        LoadNewNode(__instance, node);
        return false;
    }

    public static TerminalNode LoadNewNodeIfAffordable(Terminal term, TerminalNode node) {
        lastLoadedNode = null;
        // make sure LoadNewNode is redirected to TerminalPatch.LoadNewNode
        bool prev = _redirectLoadNewNode;
        _redirectLoadNewNode = true;
        // execute vanilla method
        term.LoadNewNodeIfAffordable(node);
        // undo previous set
        _redirectLoadNewNode = prev;
        return lastLoadedNode;
    }
    
    // ReSharper disable once MemberCanBePrivate.Global
    public static void LoadNewNode(Terminal term, TerminalNode node) {
        term.RunTerminalEvents(node);
        term.screenText.interactable = true;
        lastLoadedNode = node;
        if (node.playSyncedClip != -1)
            term.PlayTerminalAudioServerRpc(node.playSyncedClip);
        else if (node.playClip != null)
            term.terminalAudio.PlayOneShot(node.playClip);
        term.LoadTerminalImage(node);
    }

    [HarmonyPrefix]
    [HarmonyPatch(nameof(Terminal.OnSubmit))]
    public static bool OnSubmitPrefix() {
        return false; // disable OnSubmit Event in Terminal class
    }
    
    [HarmonyPrefix]
    [HarmonyPatch(nameof(Terminal.TextChanged))]
    public static bool TextChangedPrefix() {
        return false; // disable TextChanged Event in Terminal class
    }

    
    [HarmonyPrefix]
    [HarmonyPatch(nameof(Terminal.BeginUsingTerminal))]
    public static void BeginUsingTerminalPrefix(Terminal __instance) {
        _usedTerminalThisSession = __instance.usedTerminalThisSession;
    }
    
    [HarmonyPostfix]
    [HarmonyPatch(nameof(Terminal.BeginUsingTerminal))]
    public static void BeginUsingTerminalPostfix(Terminal __instance) {
        OnEnterTerminal?.Invoke(!_usedTerminalThisSession);
    }
    
    [HarmonyPrefix]
    [HarmonyPatch(nameof(Terminal.Awake))]
    public static void AwakePrefix(Terminal __instance) {
        Plugin.driver = new InputFieldDriver(__instance);
        PreAwake?.Invoke();
    }
    
    [HarmonyPostfix]
    [HarmonyPatch(nameof(Terminal.Awake))]
    public static void AwakePostfix() {
        PostAwake?.Invoke();
    }
    
    [HarmonyPrefix]
    [HarmonyPatch(nameof(Terminal.Start))]
    public static void StartPrefix() {
        PreStart?.Invoke();
    }
    
    [HarmonyPostfix]
    [HarmonyPatch(nameof(Terminal.Start))]
    public static void StartPostfix() {
        PostStart?.Invoke();
    }
    
    [HarmonyPrefix]
    [HarmonyPatch(nameof(Terminal.Update))]
    public static void UpdatePrefix() {
        PreUpdate?.Invoke();
    }
    
    [HarmonyPostfix]
    [HarmonyPatch(nameof(Terminal.Update))]
    public static void UpdatePostfix() {
        PostUpdate?.Invoke();
    }
}