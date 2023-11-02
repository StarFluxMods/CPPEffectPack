using ChefPlusPlus.Effects;
using ChefPlusPlus.Enums;
using ChefPlusPlus.Interfaces;
using ChefPlusPlus.Utils;
using CPPEffectsPack.Systems;

namespace CPPEffectsPack.Effects
{
    public class EMessCreator : BaseEffect, IEffect
    {
        //General Settings
        public string Count = "0";
        public string IsCustomerMess = "false";
        public string Range = "0";
        public string Type => GetType().FullName;

        public OnTriggerResult OnTrigger(ITrigger trigger, object args)
        {
            string NewCount = trigger.Replace(Count, args);
            string NewIsCustomerMess = trigger.Replace(IsCustomerMess, args);
            string NewRange = trigger.Replace(Range, args);

            if (!int.TryParse(NewCount, out int count))
                return new OnTriggerResult(OnTriggerResultType.Failure, "Count is not a valid int");

            if (!bool.TryParse(NewIsCustomerMess, out bool customerMess))
                return new OnTriggerResult(OnTriggerResultType.Failure, "IsCustomerMess is not a valid bool");

            if (!int.TryParse(NewRange, out int range))
                return new OnTriggerResult(OnTriggerResultType.Failure, "Range is not a valid int");

            MessCreator.MessSets.Add(new MessSet(count, customerMess, range));
            return new OnTriggerResult(OnTriggerResultType.Success);
        }
    }
}