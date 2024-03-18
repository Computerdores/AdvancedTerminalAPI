using System.Collections.Generic;
using BepInEx;
using HarmonyLib;
using UnityEngine;

namespace Computerdores.patch;

// Note: This whole thing will silently fail if there are ever more than 2 terminals in the level
[HarmonyPatch(typeof(Terminal))]
public class TerminalPatch {

    public static event InputFieldDriver.EnterTerminalEvent OnEnterTerminal;
    
    [HarmonyPostfix]
    [HarmonyPatch("Start")]
    public static void StartPostfix(Terminal __instance) {
        Plugin.Driver = new InputFieldDriver(__instance);
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

    [HarmonyPostfix]
    [HarmonyPatch("BeginUsingTerminal")]
    public static void BeginUsingTerminalPostfix(Terminal __instance) {
        OnEnterTerminal?.Invoke(!__instance.usedTerminalThisSession);
    }
}