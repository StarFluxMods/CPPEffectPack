using ChefPlusPlus.Effects;
using ChefPlusPlus.Enums;
using ChefPlusPlus.Interfaces;
using ChefPlusPlus.Utils;
using CPPEffectsPack.Systems;

namespace CPPEffectsPack.Effects
{
    public class ESpookyBrooms : BaseEffect, IEffect
    {
        //General Settings
        public string Count = "0";
        public string Type => GetType().FullName;

        public OnTriggerResult OnTrigger(ITrigger trigger, object args)
        {
            string NewCount = trigger.Replace(Count, args);

            if (!int.TryParse(NewCount, out int _Count))
                return new OnTriggerResult(OnTriggerResultType.Failure, "Count is not a valid int");

            SpookyBrooms.BroomCount += _Count;
            //TemporarySpookyBrooms.counter += _Count;
            return new OnTriggerResult(OnTriggerResultType.Success);
        }
    }
}