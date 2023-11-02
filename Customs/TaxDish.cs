using System.Collections.Generic;
using KitchenData;
using KitchenLib.Customs;

namespace CPPEffectsPack.Customs
{
    public class TaxDish : CustomDish
    {
        public override string UniqueNameID => "TaxDish";

        public override List<(Locale, UnlockInfo)> InfoList => new()
        {
            (
                Locale.English,
                new UnlockInfo
                {
                    Name = "Taxes"
                }
            )
        };
    }
}