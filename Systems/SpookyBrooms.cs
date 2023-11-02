using CPPEffectsPack.Components;
using Kitchen;
using KitchenMods;
using Unity.Entities;
using UnityEngine;

namespace CPPEffectsPack.Systems
{
    public class SpookyBrooms : DaySystem, IModSystem
    {
        public static int BroomCount;
        private EntityQuery brooms;

        protected override void Initialise()
        {
            brooms = GetEntityQuery(typeof(CManualBroom), typeof(CAppliance));
        }

        protected override void OnUpdate()
        {
            if (!HasSingleton<SIsDayTime>())
                return;

            if (BroomCount > 0)
            {
                CLayoutRoomTile tile = Tiles[Random.Range(0, Tiles.Length)];
                if (GetOccupant(tile.Position) != Entity.Null)
                    return;
                Entity entity = EntityManager.CreateEntity(typeof(CCreateAppliance), typeof(CPosition), typeof(CDestroyApplianceAtNight), typeof(CManualBroom));
                EntityManager.SetComponentData(entity, new CCreateAppliance
                {
                    ID = -877630314
                });
                EntityManager.SetComponentData(entity, new CPosition
                {
                    Position = tile.Position
                });
                BroomCount--;
            }
        }
    }
}