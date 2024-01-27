using System.Reflection;
using ChefPlusPlus.Utils;
using CPPEffectsPack.Customs;
using CPPEffectsPack.Effects;
using KitchenLib;
using KitchenMods;
using UnityEngine;

namespace CPPEffectsPack
{
    public class Mod : BaseMod, IModSystem
    {
        public const string MOD_GUID = "com.starfluxgames.cppeffectpack";
        public const string MOD_NAME = "CPP Effect Pack";
        public const string MOD_VERSION = "0.1.0";
        public const string MOD_BETA_VERSION = "8";
        public const string MOD_AUTHOR = "StarFluxGames";
        public const string MOD_GAMEVERSION = ">=1.1.8";

        public static TaxDish TaxDish;

        public static AssetBundle Bundle;

        public Mod() : base(MOD_GUID, MOD_NAME, MOD_AUTHOR, MOD_VERSION, MOD_BETA_VERSION, MOD_GAMEVERSION, Assembly.GetExecutingAssembly())
        {
        }

        protected override void OnInitialise()
        {
            LogWarning($"{MOD_GUID} v{MOD_VERSION} in use!");
        }

        protected override void OnPostActivate(KitchenMods.Mod mod)
        {
            TaxDish = AddGameDataObject<TaxDish>();
            Loader.RegisterEffect<EAdjustMoney>();
            // Loader.RegisterEffect<ECookingSpeed>();
            Loader.RegisterEffect<EExtraCustomers>();
            Loader.RegisterEffect<EFasterPlayers>();
            Loader.RegisterEffect<EGrabberSpeed>();
            Loader.RegisterEffect<ELightFire>();
            Loader.RegisterEffect<EMessCreator>();
            Loader.RegisterEffect<ERenameRestaurant>();
            Loader.RegisterEffect<ESpookyBrooms>();
            Loader.RegisterEffect<ETax>();
            Loader.RegisterEffect<ETemporarySpookyBrooms>();
        }

        #region Logging

        public static void LogInfo(string _log)
        {
            Debug.Log($"[{MOD_NAME}] " + _log);
        }

        public static void LogWarning(string _log)
        {
            Debug.LogWarning($"[{MOD_NAME}] " + _log);
        }

        public static void LogError(string _log)
        {
            Debug.LogError($"[{MOD_NAME}] " + _log);
        }

        public static void LogInfo(object _log)
        {
            LogInfo(_log.ToString());
        }

        public static void LogWarning(object _log)
        {
            LogWarning(_log.ToString());
        }

        public static void LogError(object _log)
        {
            LogError(_log.ToString());
        }

        #endregion
    }
}