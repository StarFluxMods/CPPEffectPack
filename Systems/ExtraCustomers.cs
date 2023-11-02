using System.Collections.Generic;
using Kitchen;
using KitchenData;
using KitchenLib.References;
using KitchenMods;

namespace CPPEffectsPack.Systems
{
    public class ExtraCustomers : CreateCustomerGroup, IModSystem
    {
        public static List<CustomerSet> CustomerSets = new();

        protected override void OnUpdate()
        {
            if (CustomerSets.Count == 0)
                return;

            if (!HasSingleton<SIsDayTime>())
                CustomerSets.Clear();

            for (int i = 0; i < CustomerSets.Count; i++)
            {
                NewGroup(GameData.Main.Get<CustomerType>(CustomerTypeReferences.GenericCustomer), CustomerSets[i].Count, CustomerSets[i].IsCat);
                CustomerSets.RemoveAt(i);
            }
        }
    }

    public class CustomerSet
    {
        public int Count;
        public bool IsCat;

        public CustomerSet(int count, bool isCat)
        {
            Count = count;
            IsCat = isCat;
        }
    }
}