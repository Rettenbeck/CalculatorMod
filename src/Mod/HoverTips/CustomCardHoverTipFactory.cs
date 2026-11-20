using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Entities.Cards;
using System.Collections.Generic;

namespace CalculatorMod;


public static class CustomCardHoverTipFactory
{
    public static List<IHoverTip> createHoverTips(CardModel __instance)
    {
        List<IHoverTip> result = new List<IHoverTip>();
        
        var customHoverTip = createHoverTip(__instance);
        if (customHoverTip != null) result.Add(customHoverTip);
        
        return result;
    }

    private static HoverTip? createHoverTip(CardModel cardModel)
    {
        if(cardModel == null) return null;
        CalcManager.UpdateInternalCombatState();

        var cardId = cardModel.Id.ToString();
        var cardTitle = cardModel.Title;
        var cardPile = CalcManager.FindCard(cardModel);

        var id = "";
        id = "CardId:" + cardId +
                "\n" + "Title: " + cardTitle +
                "\n" + "Pile: " + cardPile;

        string show = "No state";
        if(CalcManager.CurrentCombatState != null)
        {
            show = "Time: " + CalcManager.CurrentCombatState.TimeStamp;
            show += "\n" + "Hp: " + CalcManager.CurrentCombatState.PlayerHp;
            show += "\n" + "MaxHp: " + CalcManager.CurrentCombatState.PlayerMaxHp;
            show += "\n" + "Block: " + CalcManager.CurrentCombatState.PlayerBlock;
            show += "\n" + "Energy: " + CalcManager.CurrentCombatState.Energy;
            show += "\n" + "MaxEnergy: " + CalcManager.CurrentCombatState.MaxEnergy;
            show += "\n" + "Gold: " + CalcManager.CurrentCombatState.PlayerGold;
        }

        var title = "Card title";
        var desc = id + "\n[color=FF6937]" + show + "[/color]";

        return HoverTipHelper.Create(title, desc);
    }

}
