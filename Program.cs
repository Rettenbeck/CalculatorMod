using HarmonyLib;
using MegaCrit.Sts2.Core.Modding;
using MegaCrit.Sts2.Core.Logging;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CalculatorMod;


[ModInitializer("ModLoaded")] 
public static class CalculatorMod 
{ 
    public static async Task ModLoaded() 
    {
        DoHarmonyPatches();
    }

    public static void DoHarmonyPatches()
    {
        MLog.Info($"Initializing Harmony Patches for Calculator Mod... ");
        var harmony = new Harmony("com.statsmod.patch");
        harmony.PatchAll();
        MLog.Info($"Ready!");
    }

    public static string GetModPath()
    {
        return Path.GetDirectoryName(typeof(CalculatorMod).Assembly.Location) + "/";
    }

}
