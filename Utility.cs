using System;
using CPPEffectsPack.Systems;
using KitchenData;

namespace CPPEffectsPack
{
    public class Utility
    {
        public static float GetGrabberSpeed()
        {
            return GrabberSpeed.Grabber_Speed;
        }

        public static float GetGrabberSpeedWithHalloweenCard()
        {
            return GrabberSpeed.Grabber_Speed / 2;
        }

        public static float GetCookSpeed(float x, ApplianceProcessPair pair)
        {
            if (CookingSpeed.AffectBadProcesses)
                return x * CookingSpeed.Cooking_Speed;
            if (!pair.IsBad)
                return x * CookingSpeed.Cooking_Speed;
            return x;
        }

        public static int ChangeMoney(int money)
        {
            float percent = TemporaryTax.current_tax / 100.0f;
            float tax = percent * money;
            AdjustMoney.MoneyTracks.Add(new MoneyTrack(Mod.TaxDish.ID, -(int)Math.Ceiling(tax)));
            return money;
        }
    }
}