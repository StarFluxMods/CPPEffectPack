using ChefPlusPlus.Effects;
using ChefPlusPlus.Enums;
using ChefPlusPlus.Interfaces;
using ChefPlusPlus.Utils;
using CPPEffectsPack.Systems;

namespace CPPEffectsPack.Effects
{
    public class ETemporarySpookyBrooms : BaseEffect, IEffect
    {
        //General Settings
        public string Count = "0";
        public string SecondsToLive = "0";
        public string Type => GetType().FullName;

        public OnTriggerResult OnTrigger(ITrigger trigger, object args)
        {
            string NewCount = trigger.Replace(Count, args);
            string NewSecondsToLive = trigger.Replace(SecondsToLive, args);

            if (!int.TryParse(NewCount, out int _Count))
                return new OnTriggerResult(OnTriggerResultType.Failure, "Count is not a valid int");

            if (!int.TryParse(NewSecondsToLive, out int _SecondsToLive))
                return new OnTriggerResult(OnTriggerResultType.Failure, "SecondsToLive is not a valid int");

            TemporarySpookyBrooms.BroomSets.Add(new TemporaryBroomSet(_Count, _SecondsToLive));
            return new OnTriggerResult(OnTriggerResultType.Success);
        }
    }
}