using ChefPlusPlus.Effects;
using ChefPlusPlus.Enums;
using ChefPlusPlus.Interfaces;
using ChefPlusPlus.Utils;
using CPPEffectsPack.Systems;

namespace CPPEffectsPack.Effects
{
    public class ECookingSpeed : BaseEffect, IEffect
    {
        public string AffectBadProcesses = "false";

        public string MultiplierA = "1";
        public string MultiplierB = "1";
        public string Seconds = "0";
        public string Type => GetType().FullName;

        public OnTriggerResult OnTrigger(ITrigger trigger, object args)
        {
            string NewMultiplierA = trigger.Replace(MultiplierA, args);
            string NewMultiplierB = trigger.Replace(MultiplierB, args);
            string NewSeconds = trigger.Replace(Seconds, args);
            string NewAffectBadProcesses = trigger.Replace(AffectBadProcesses, args);

            if (!float.TryParse(NewMultiplierA, out float _MultiplierA))
                return new OnTriggerResult(OnTriggerResultType.Failure, "MultiplierA is not a valid float");

            if (!float.TryParse(NewMultiplierB, out float _MultiplierB))
                return new OnTriggerResult(OnTriggerResultType.Failure, "MultiplierB is not a valid float");

            if (!int.TryParse(NewSeconds, out int _Seconds))
                return new OnTriggerResult(OnTriggerResultType.Failure, "Seconds is not a valid int");

            if (!bool.TryParse(NewAffectBadProcesses, out bool _AffectBadProcesses))
                return new OnTriggerResult(OnTriggerResultType.Failure, "AffectBadProcesses is not a valid bool");

            CookingSpeed.SpeedSets.Add((new SpeedSet(_MultiplierA, _MultiplierB, _Seconds), _AffectBadProcesses));
            return new OnTriggerResult(OnTriggerResultType.Success);
        }
    }
}