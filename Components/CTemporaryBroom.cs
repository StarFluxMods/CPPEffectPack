using KitchenData;
using KitchenMods;
using Unity.Entities;

namespace CPPEffectsPack.Components
{
    public struct CTemporaryBroom : IApplianceProperty, IAttachableProperty, IComponentData, IModComponent
    {
        public int TimeSpawned;
        public int SecondsToLive;
    }
}