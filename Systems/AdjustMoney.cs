using System.Collections.Generic;
using Kitchen;
using KitchenMods;
using Unity.Entities;

namespace CPPEffectsPack.Systems
{
    public class AdjustMoney : RestaurantSystem, IModSystem
    {
        public static List<MoneyTrack> MoneyTracks = new();

        protected override void Initialise()
        {
        }

        protected override void OnUpdate()
        {
            if (MoneyTracks.Count == 0)
                return;

            for (int i = 0; i < MoneyTracks.Count; i++)
            {
                MoneyTrack moneyTrack = MoneyTracks[i];
                if (HasSingleton<SMoney>())
                {
                    SMoney money = GetSingleton<SMoney>();
                    money.Amount += moneyTrack.Amount;
                    SetSingleton(money);
                }

                Entity e = EntityManager.CreateEntity(typeof(CMoneyTrackEvent));
                EntityManager.SetComponentData(e, new CMoneyTrackEvent
                {
                    Identifier = moneyTrack.Id,
                    Amount = moneyTrack.Amount
                });

                MoneyTracks.RemoveAt(i);
            }
        }
    }

    public class MoneyTrack
    {
        public int Amount;
        public int Id;

        public MoneyTrack(int id, int amount)
        {
            Id = id;
            Amount = amount;
        }
    }
}