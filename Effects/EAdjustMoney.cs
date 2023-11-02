using ChefPlusPlus.Effects;
using ChefPlusPlus.Enums;
using ChefPlusPlus.Interfaces;
using ChefPlusPlus.Utils;
using CPPEffectsPack.Systems;

namespace CPPEffectsPack.Effects
{
    public class EAdjustMoney : BaseEffect, IEffect
    {
        //General Settings
        public string Amount = "0";
        public string Type => GetType().FullName;

        public OnTriggerResult OnTrigger(ITrigger trigger, object args)
        {
            string NewAmount = trigger.Replace(Amount, args);

            if (!int.TryParse(NewAmount, out int _Amount))
                return new OnTriggerResult(OnTriggerResultType.Failure, "Amount is not a valid int");

            AdjustMoney.MoneyTracks.Add(new MoneyTrack(Mod.TaxDish.ID, _Amount));
            return new OnTriggerResult(OnTriggerResultType.Success);
        }
    }
}