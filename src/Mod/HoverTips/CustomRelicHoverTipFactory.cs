using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Entities.Cards;
using System.Collections.Generic;

namespace CalculatorMod;


public static class CustomRelicHoverTipFactory
{
    public static List<IHoverTip> createHoverTips(RelicModel __instance)
    {
        List<IHoverTip> result = new List<IHoverTip>();
        
        var customHoverTip1 = createHoverTip(__instance);
        if (customHoverTip1 != null) result.Add(customHoverTip1);
        
        var customHoverTip2 = createHoverTipCreatures(__instance);
        if (customHoverTip2 != null) result.Add(customHoverTip2);
        
        return result;
    }

    private static HoverTip? createHoverTip(RelicModel relicModel)
    {
        if(relicModel == null) return null;
        CalcManager.UpdateInternalCombatState();

        var title = "Relic title";
        var desc = relicModel.Id.ToString();

        return HoverTipHelper.Create(title, desc);
    }

    private static HoverTip? createHoverTipCreatures(RelicModel relicModel)
    {
        CalcManager.UpdateInternalCombatState();

        var state = CalcManager.CurrentGameCombatState;
        if(state == null) return null;

        var player = CalcManager.GetPlayer();
        if(player == null) return null;

        var title = "Creatures";
        var desc = "Player: " + player.Character.Id.ToString() + "\n\nEnemies:";

        var enemies = state.Enemies;
        foreach(var enemy in enemies)
        {
            desc += "\n" + enemy.ModelId.ToString();
            var monster = enemy.Monster;
            if(monster != null)
            {
                var nextMove = monster.NextMove;
                var intents = nextMove.Intents;
                var moveId = nextMove.Id;
                desc += " (Move: " + moveId + ")";
                foreach(var intent in intents)
                {
                    desc += "\n  -> " + intent.IntentType;
                }
            }
        }

        return HoverTipHelper.Create(title, desc);
    }

}
