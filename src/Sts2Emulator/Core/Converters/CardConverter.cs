using MegaCrit.Sts2.Core.Models;

namespace Sts2Emulator.Core;


public static class CardConverter
{
    public static ConversionMap Map = CreateMapping();

    public static CardInstance Get(CardModel cardModel)
    {
        int DefId = Map.Find(cardModel.Id.ToString());
        bool Upgraded = cardModel.CurrentUpgradeLevel > 0;
        bool FreeThisTurn = false;
        bool Retain = false;
        int Sharp = 0;
        int Nimble = 0;
        int Swift = 0;
        int CostForCombat = int.MinValue;

        return new CardInstance(
            DefId, Upgraded, FreeThisTurn, Retain, Sharp, Nimble, Swift, CostForCombat
        );
    }

    public static ConversionMap CreateMapping()
    {
        var map = new ConversionMap();

        map.Add(Effects.IC.StrikeIronclad       , "CARD.STRIKE_IRONCLAD"        );
        map.Add(Effects.IC.DefendIronclad       , "CARD.DEFEND_IRONCLAD"        );
        map.Add(Effects.IC.Bash                 , "CARD.BASH"                   );
        map.Add(Effects.IC.Uppercut             , "CARD.UPPERCUT"               );

        return map;
    }
}
