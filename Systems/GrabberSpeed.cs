using System;
using System.Collections.Generic;
using Kitchen;
using KitchenMods;

namespace CPPEffectsPack.Systems
{
    public class GrabberSpeed : GameSystemBase, IModSystem
    {
        public static List<SpeedSet> SpeedSets = new();
        public static float Grabber_Speed = 1;

        private readonly float base_speed = 1;
        private int Last_Triggered_Length = 1;

        private int Last_Triggered_Time;

        protected override void OnUpdate()
        {
            if (Last_Triggered_Time + Last_Triggered_Length < DateTimeOffset.Now.ToUnixTimeSeconds())
            {
                Grabber_Speed = base_speed;

                if (SpeedSets.Count == 0)
                    return;

                SpeedSet set = SpeedSets[0];
                Grabber_Speed = set.MultiplierA * set.MultiplierB;

                Last_Triggered_Time = (int)DateTimeOffset.Now.ToUnixTimeSeconds();
                Last_Triggered_Length = set.Seconds;
                SpeedSets.RemoveAt(0);
            }
        }
    }
}