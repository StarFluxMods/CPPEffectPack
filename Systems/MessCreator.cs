using System.Collections.Generic;
using Kitchen;
using KitchenLib.References;
using KitchenMods;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace CPPEffectsPack.Systems
{
    public class MessCreator : GameSystemBase, IModSystem
    {
        public static List<MessSet> MessSets = new();

        private EntityQuery players;

        protected override void Initialise()
        {
            players = GetEntityQuery(typeof(CPlayer));
        }

        protected override void OnUpdate()
        {
            if (MessSets.Count == 0)
                return;

            if (players.CalculateEntityCount() == 0)
                MessSets.Clear();

            NativeArray<Entity> playerArray = players.ToEntityArray(Allocator.TempJob);
            for (int i = 0; i < MessSets.Count; i++)
                if (Require(playerArray[Random.Range(0, playerArray.Length)], out CPosition position))
                {
                    int messid = 0;
                    if (MessSets[i].IsCustomer)
                        messid = ApplianceReferences.MessCustomer1;
                    else
                        messid = ApplianceReferences.MessKitchen1;

                    for (int x = 0; x < MessSets[i].Count; x++)
                    {
                        Entity mess = EntityManager.CreateEntity(typeof(CMessRequest), typeof(CPosition));
                        EntityManager.SetComponentData(mess, new CMessRequest
                        {
                            ID = messid,
                            OverwriteOtherMesses = false
                        });
                        CPosition newPosition = new();
                        newPosition.Position = position.Position;

                        int range = MessSets[i].Range;

                        newPosition.Position.x += Random.Range(-range, range);
                        newPosition.Position.z += Random.Range(-range, range);

                        EntityManager.SetComponentData(mess, newPosition);
                    }

                    MessSets.RemoveAt(i);
                }

            playerArray.Dispose();
        }
    }

    public class MessSet
    {
        public int Count;
        public bool IsCustomer;
        public int Range;

        public MessSet(int count, bool isCustomer, int range)
        {
            Count = count;
            IsCustomer = isCustomer;
            Range = range;
        }
    }
}