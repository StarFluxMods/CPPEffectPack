using ChefPlusPlus.Effects;
using ChefPlusPlus.Enums;
using ChefPlusPlus.Interfaces;
using ChefPlusPlus.Utils;
using CPPEffectsPack.Systems;
using TwitchLib.PubSub.Events;

namespace CPPEffectsPack.Effects
{
    public class EExtraCustomers : BaseEffect, IEffect
    {
        // Bits Settings
        public string BitsForCat = "0";

        //General Settings
        public string Count = "0";
        public string IsCat = "false";
        public string Type => GetType().FullName;

        public OnTriggerResult OnTrigger(ITrigger trigger, object args)
        {
            string NewCount = trigger.Replace(Count, args);
            string NewIsCat = trigger.Replace(IsCat, args);

            if (trigger.trigger == EffectTrigger.OnBitsReceivedV2)
                if (int.TryParse(BitsForCat, out int bitsForCat))
                    if (((OnBitsReceivedV2Args)args).BitsUsed >= bitsForCat)
                        IsCat = "true";


            if (!int.TryParse(NewCount, out int count))
                return new OnTriggerResult(OnTriggerResultType.Failure, "Count is not a valid int");

            if (!bool.TryParse(NewIsCat, out bool cat))
                return new OnTriggerResult(OnTriggerResultType.Failure, "IsCat is not a valid bool");

            ExtraCustomers.CustomerSets.Add(new CustomerSet(count, cat));
            return new OnTriggerResult(OnTriggerResultType.Success);
        }
    }
}