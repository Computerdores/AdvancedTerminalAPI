using System.Collections.Generic;
using BepInEx;
using HarmonyLib;
using UnityEngine;

namespace Computerdores.patch;

[HarmonyPatch(typeof(Terminal))]
public class TerminalPatch {

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
}