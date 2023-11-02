using ChefPlusPlus.Effects;
using ChefPlusPlus.Enums;
using ChefPlusPlus.Interfaces;
using ChefPlusPlus.Utils;
using CPPEffectsPack.Systems;

namespace CPPEffectsPack.Effects
{
    public class ETax : BaseEffect, IEffect
    {
        //General Settings
        public string Percentage = "0";
        public string Seconds = "0";
        public string Type => GetType().FullName;

        public OnTriggerResult OnTrigger(ITrigger trigger, object args)
        {
            string NewPercentage = trigger.Replace(Percentage, args);
            string NewSeconds = trigger.Replace(Seconds, args);

            if (!float.TryParse(NewPercentage, out float _Percentage))
                return new OnTriggerResult(OnTriggerResultType.Failure, "Percentage is not a valid float");

            if (!int.TryParse(NewSeconds, out int _Seconds))
                return new OnTriggerResult(OnTriggerResultType.Failure, "Seconds is not a valid int");

            TemporaryTax.TaxSets.Add(new TaxSet(_Percentage, _Seconds));
            return new OnTriggerResult(OnTriggerResultType.Success);
        }
    }
}