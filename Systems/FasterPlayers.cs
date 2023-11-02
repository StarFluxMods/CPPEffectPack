using System;
using System.Collections.Generic;
using Kitchen;
using KitchenMods;
using Unity.Collections;
using Unity.Entities;

namespace CPPEffectsPack.Systems
{
    [UpdateAfter(typeof(DeterminePlayerSpeed))]
    public class FasterPlayers : GameSystemBase, IModSystem
    {
        public static List<SpeedSet> SpeedSets = new();

        private readonly float base_speed = 1;
        private float currentSpeed = 1;
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
                currentSpeed = base_speed;

                if (players.CalculateEntityCount() == 0)
                    return;

                if (SpeedSets.Count == 0)
                    return;

                SpeedSet set = SpeedSets[0];
                currentSpeed = set.MultiplierA * set.MultiplierB + base_speed;

                Last_Triggered_Time = (int)DateTimeOffset.Now.ToUnixTimeSeconds();
                Last_Triggered_Length = set.Seconds;
                SpeedSets.RemoveAt(0);
            }

            NativeArray<Entity> _players = players.ToEntityArray(Allocator.Temp);
            for (int i = 0; i < _players.Length; i++)
                if (Require(_players[i], out CPlayer player))
                    if (player.Speed != currentSpeed)
                    {
                        player.Speed = currentSpeed;
                        EntityManager.SetComponentData(_players[i], player);
                    }

            _players.Dispose();
        }
    }

    public class SpeedSet
    {
        public float MultiplierA;
        public float MultiplierB;
        public int Seconds;

        public SpeedSet(float multiplierA, float multiplierB, int seconds)
        {
            MultiplierA = multiplierA;
            MultiplierB = multiplierB;
            Seconds = seconds;
        }
    }
}