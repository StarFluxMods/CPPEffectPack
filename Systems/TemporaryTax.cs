using System;
using System.Collections.Generic;
using Kitchen;
using KitchenMods;
using Unity.Entities;

namespace CPPEffectsPack.Systems
{
    [UpdateAfter(typeof(DeterminePlayerSpeed))]
    public class TemporaryTax : GameSystemBase, IModSystem
    {
        public static List<TaxSet> TaxSets = new();
        public static float current_tax = 1;

        private readonly float base_tax = 0;
        private int Last_Triggered_Length = 1;

        private int Last_Triggered_Time;
        private EntityQuery players;

        protected override void Initialise()
        {
            players = GetEntityQuery(new QueryHelper().All(typeof(CPlayer)));
        }

        protected override void OnUpdate()
        {
            if (Last_Triggered_Time + Last_Triggered_Length < DateTimeOffset.Now.ToUnixTimeSeconds())
            {
                current_tax = base_tax;

                if (players.CalculateEntityCount() == 0)
                    return;

                if (TaxSets.Count == 0)
                    return;

                TaxSet set = TaxSets[0];
                current_tax = set.Percentage;

                Last_Triggered_Time = (int)DateTimeOffset.Now.ToUnixTimeSeconds();
                Last_Triggered_Length = set.Seconds;
                TaxSets.RemoveAt(0);
            }
        }
    }

    public class TaxSet
    {
        public float Percentage;
        public int Seconds;

        public TaxSet(float percentage, int seconds)
        {
            Percentage = percentage;
            Seconds = seconds;
        }
    }
}