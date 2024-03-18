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

}