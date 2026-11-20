using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Entities.Cards;
using System.Collections.Generic;

namespace CalculatorMod;


public static class CustomPotionHoverTipFactory
{
    public static List<IHoverTip> createHoverTips(PotionModel __instance)
    {
        List<IHoverTip> result = new List<IHoverTip>();
        
        var customHoverTip = createHoverTip(__instance);
        if (customHoverTip != null) result.Add(customHoverTip);
        
        return result;
    }

    private static HoverTip? createHoverTip(PotionModel potionModel)
    {
        if(potionModel == null) return null;
        CalcManager.UpdateInternalCombatState();

        var title = "Potion title";
        var desc = potionModel.Id.ToString();

        return HoverTipHelper.Create(title, desc);
    }

}
