using HarmonyLib;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace CalculatorMod;


[HarmonyPatch(typeof(PotionModel), "get_HoverTips")]
public static class PotionHoverTipPatch
{
    public static IEnumerable<IHoverTip> Postfix(IEnumerable<IHoverTip> __result, PotionModel __instance)
    {
        List<IHoverTip> list = __result.ToList();

        var customList = CustomPotionHoverTipFactory.createHoverTips(__instance);
        list.AddRange(customList);

        return list;
    }
}
