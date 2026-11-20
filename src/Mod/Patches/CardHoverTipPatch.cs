using HarmonyLib;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace CalculatorMod;


[HarmonyPatch(typeof(CardModel), "get_HoverTips")]
public static class CardHoverTipPatch
{
    public static IEnumerable<IHoverTip> Postfix(IEnumerable<IHoverTip> __result, CardModel __instance)
    {
        List<IHoverTip> list = __result.ToList();

        var customList = CustomCardHoverTipFactory.createHoverTips(__instance);
        list.AddRange(customList);

        return list;
    }
}
