using ChefPlusPlus.Effects;
using ChefPlusPlus.Enums;
using ChefPlusPlus.Interfaces;
using ChefPlusPlus.Utils;
using CPPEffectsPack.Systems;

namespace CPPEffectsPack.Effects
{
    public class ELightFire : BaseEffect, IEffect
    {
        //General Settings
        public string Count = "0";
        public string Type => GetType().FullName;

        public OnTriggerResult OnTrigger(ITrigger trigger, object args)
        {
            string NewCount = trigger.Replace(Count, args);

            if (!int.TryParse(NewCount, out int count))
                return new OnTriggerResult(OnTriggerResultType.Failure, "Count is not a valid int");

            LightFire.Fires += count;
            return new OnTriggerResult(OnTriggerResultType.Success);
        }
    }
}