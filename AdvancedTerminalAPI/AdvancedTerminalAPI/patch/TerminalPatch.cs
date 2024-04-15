using HarmonyLib;

namespace Computerdores.AdvancedTerminalAPI.patch;

// Note: This whole thing will silently fail if there are ever more than 2 terminals in the level
[HarmonyPatch(typeof(Terminal))]
public static class TerminalPatch {
    internal static event Consumer<(bool, Terminal)> OnEnterTerminal;
    internal static event Consumer<Terminal> OnExitTerminal;

    internal static event Consumer<Terminal> PreAwake;
    internal static event Consumer<Terminal> PostAwake;
    internal static event Consumer<Terminal> PreStart;
    internal static event Consumer<Terminal> PostStart;
    internal static event Consumer<Terminal> PreUpdate;
    internal static event Consumer<Terminal> PostUpdate;

    [HarmonyPrefix]
    [HarmonyPatch(nameof(Terminal.LoadNewNode))]
    private static bool InterceptLoadNewNode(this Terminal __instance, TerminalNode node) {
        TerminalWrapper wrapper = TerminalWrapper.Get(__instance);
        if (!wrapper.redirectLoadNewNode) return true;
        wrapper.LoadNewNode(node);
        return false;
    }

    [HarmonyPrefix]
    [HarmonyPatch(nameof(Terminal.OnSubmit))]
    private static bool OnSubmitPrefix() {
        return false; // disable OnSubmit Event in Terminal class
    }

    [HarmonyPrefix]
    [HarmonyPatch(nameof(Terminal.TextChanged))]
    private static bool TextChangedPrefix() {
        return false; // disable TextChanged Event in Terminal class
    }

    [HarmonyPostfix]
    [HarmonyPatch(nameof(Terminal.BeginUsingTerminal))]
    private static void BeginUsingTerminalPostfix(Terminal __instance)
        => OnEnterTerminal?.Invoke((!__instance.usedTerminalThisSession, __instance));

    [HarmonyPrefix]
    [HarmonyPatch(nameof(Terminal.QuitTerminal))]
    private static void QuitTerminalPrefix(Terminal __instance) 
        => OnExitTerminal?.Invoke(__instance);

    [HarmonyPrefix]
    [HarmonyPatch(nameof(Terminal.Awake))]
    private static void AwakePrefix(Terminal __instance) {
        Plugin.RegisterTerminal(__instance);
        PreAwake?.Invoke(__instance);
    }

    [HarmonyPostfix]
    [HarmonyPatch(nameof(Terminal.Awake))]
    private static void AwakePostfix(Terminal __instance) 
        => PostAwake?.Invoke(__instance);

    [HarmonyPrefix]
    [HarmonyPatch(nameof(Terminal.Start))]
    private static void StartPrefix(Terminal __instance)
        => PreStart?.Invoke(__instance);

    [HarmonyPostfix]
    [HarmonyPatch(nameof(Terminal.Start))]
    private static void StartPostfix(Terminal __instance)
        => PostStart?.Invoke(__instance);

    [HarmonyPrefix]
    [HarmonyPatch(nameof(Terminal.Update))]
    private static void UpdatePrefix(Terminal __instance) 
        => PreUpdate?.Invoke(__instance);

    [HarmonyPostfix]
    [HarmonyPatch(nameof(Terminal.Update))]
    private static void UpdatePostfix(Terminal __instance)
        => PostUpdate?.Invoke(__instance);
}