using System;
using System.Collections.Generic;
using Kitchen;
using KitchenMods;

namespace CPPEffectsPack.Systems
{
    public class CookingSpeed : GameSystemBase, IModSystem
    {
        public static List<(SpeedSet, bool)> SpeedSets = new();
        public static float Cooking_Speed = 1;
        public static bool AffectBadProcesses = true;

        private readonly float base_speed = 1;
        private int Last_Triggered_Length = 1;

        private int Last_Triggered_Time;

        protected override void OnUpdate()
        {
            if (Last_Triggered_Time + Last_Triggered_Length < DateTimeOffset.Now.ToUnixTimeSeconds())
            {
                Cooking_Speed = base_speed;

                if (SpeedSets.Count == 0)
                    return;

                SpeedSet set = SpeedSets[0].Item1;
                AffectBadProcesses = SpeedSets[0].Item2;
                Cooking_Speed = set.MultiplierA * set.MultiplierB;

                Last_Triggered_Time = (int)DateTimeOffset.Now.ToUnixTimeSeconds();
                Last_Triggered_Length = set.Seconds;
                SpeedSets.RemoveAt(0);
            }
        }
    }
}