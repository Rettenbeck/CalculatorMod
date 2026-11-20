using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization;

namespace CalculatorMod;


public static class HoverTipHelper
{
    public static HoverTip Create(string title, string desc)
    {
        var LocStringTitle = new LocString(Constants.LOC_TABLE_HOVERTIP, Constants.LOC_TITLE);
        var LocStringDesc  = new LocString(Constants.LOC_TABLE_HOVERTIP, Constants.LOC_DESC);

        LocStringTitle.AddObj(Constants.VAR_TITLE, title);
        LocStringDesc .AddObj(Constants.VAR_DESC , desc );

        return new HoverTip(LocStringTitle, LocStringDesc);
    }
}