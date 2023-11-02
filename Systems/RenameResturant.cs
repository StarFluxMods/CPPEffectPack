using Kitchen;
using KitchenMods;
using Unity.Collections;
using Unity.Entities;

namespace CPPEffectsPack.Systems
{
    public class RenameResturant : RestaurantSystem, IModSystem
    {
        public static string NewName = "";
        private EntityQuery Nameplate;

        protected override void Initialise()
        {
            Nameplate = GetEntityQuery(typeof(CRenameRestaurant));
        }

        protected override void OnUpdate()
        {
            if (string.IsNullOrEmpty(NewName))
                return;

            NativeArray<Entity> entities = Nameplate.ToEntityArray(Allocator.TempJob);
            foreach (Entity entity in entities)
                if (Require(entity, out CRenameRestaurant rename))
                {
                    rename.Name = NewName;
                    EntityManager.SetComponentData(entity, rename);
                    NewName = "";
                }

            entities.Dispose();
        }
    }
}