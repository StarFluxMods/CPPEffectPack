using Kitchen;
using KitchenData;
using KitchenLib.References;
using KitchenMods;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace CPPEffectsPack.Systems
{
    public class LightFire : GameSystemBase, IModSystem
    {
        public static int Fires;
        private EntityQuery Appliances;

        protected override void Initialise()
        {
            Appliances = GetEntityQuery(new QueryHelper()
                .All(typeof(CAppliance))
                .None(
                    typeof(CFire),
                    typeof(CIsOnFire),
                    typeof(CFireImmune)
                ));
        }

        protected override void OnUpdate()
        {
            if (Fires == 0)
                return;

            if (Appliances.CalculateEntityCount() == 0)
                return;

            NativeArray<Entity> appliances = Appliances.ToEntityArray(Allocator.TempJob);

            Entity entity = appliances[Random.Range(0, appliances.Length)];

            if (Require(entity, out CAppliance appliance))
            {
                bool result = true;
                Appliance app = GameData.Main.Get<Appliance>(appliance.ID);
                if (app.Layer != OccupancyLayer.Default)
                    result = false;

                if (app.IsNonInteractive)
                    result = false;

                if (app.ID == ApplianceReferences.TableBar || app.ID == ApplianceReferences.TableLarge || app.ID == ApplianceReferences.TableBasicCloth || app.ID == ApplianceReferences.TableCheapMetal || app.ID == ApplianceReferences.TableFancyCloth || app.ID == ApplianceReferences.CoffeeTable)
                    result = false;

                if (result)
                {
                    EntityManager.AddComponent<CIsOnFire>(entity);
                    Fires--;
                }

                appliances.Dispose();
            }
        }
    }
}