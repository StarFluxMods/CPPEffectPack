using ChefPlusPlus.Effects;
using ChefPlusPlus.Enums;
using ChefPlusPlus.Interfaces;
using ChefPlusPlus.Utils;
using CPPEffectsPack.Systems;

namespace CPPEffectsPack.Effects
{
    public class ERenameRestaurant : BaseEffect, IEffect
    {
        //General Settings
        public string Name = "";
        public string Type => GetType().FullName;

        public OnTriggerResult OnTrigger(ITrigger trigger, object args)
        {
            string NewName = trigger.Replace(Name, args);

            if (string.IsNullOrEmpty(Name)) return new OnTriggerResult(OnTriggerResultType.Failure, "No name was given");
            if (Name == "empty?") return new OnTriggerResult(OnTriggerResultType.Failure, "User Input was not cached. Race condition?");
            RenameResturant.NewName = NewName;
            return new OnTriggerResult(OnTriggerResultType.Success);
        }
    }
}