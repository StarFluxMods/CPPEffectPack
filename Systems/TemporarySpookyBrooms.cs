using System;
using System.Collections.Generic;
using CPPEffectsPack.Components;
using Kitchen;
using KitchenMods;
using Unity.Collections;
using Unity.Entities;
using Random = UnityEngine.Random;

namespace CPPEffectsPack.Systems
{
    public class TemporarySpookyBrooms : DaySystem, IModSystem
    {
        public static List<TemporaryBroomSet> BroomSets = new();

        private EntityQuery brooms;

        protected override void Initialise()
        {
            brooms = GetEntityQuery(typeof(CTemporaryBroom), typeof(CAppliance));
        }

        protected override void OnUpdate()
        {
            NativeArray<Entity> entities = brooms.ToEntityArray(Allocator.Temp);
            for (int i = 0; i < entities.Length; i++)
            {
                Entity entity = entities[i];
                if (Require(entity, out CTemporaryBroom broom))
                    if (broom.TimeSpawned + broom.SecondsToLive < DateTimeOffset.Now.ToUnixTimeSeconds())
                        EntityManager.DestroyEntity(entity);
            }

            entities.Dispose();


            if (!HasSingleton<SIsDayTime>())
                return;

            for (int i = 0; i < BroomSets.Count; i++)
            {
                TemporaryBroomSet set = BroomSets[i];
                for (int x = 0; x < set.Count; x++)
                {
                    CLayoutRoomTile tile = Tiles[Random.Range(0, Tiles.Length)];
                    Entity entity = EntityManager.CreateEntity(typeof(CCreateAppliance), typeof(CPosition), typeof(CDestroyApplianceAtNight), typeof(CTemporaryBroom));
                    EntityManager.SetComponentData(entity, new CCreateAppliance
                    {
                        ID = -877630314
                    });
                    EntityManager.SetComponentData(entity, new CPosition
                    {
                        Position = tile.Position
                    });
                    EntityManager.SetComponentData(entity, new CTemporaryBroom
                    {
                        TimeSpawned = (int)DateTimeOffset.Now.ToUnixTimeSeconds(),
                        SecondsToLive = set.SecondsToLive
                    });
                }
            }

            BroomSets.Clear();
        }
    }

    public class TemporaryBroomSet
    {
        public int Count;
        public int SecondsToLive;

        public TemporaryBroomSet(int count, int secondsToLive)
        {
            Count = count;
            SecondsToLive = secondsToLive;
        }
    }
}