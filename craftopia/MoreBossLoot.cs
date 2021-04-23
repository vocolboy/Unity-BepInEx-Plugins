using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using Oc;
using Oc.Em;
using Oc.Network;

namespace MoreBossLoot
{
    [BepInPlugin("tw.vocolboy.plugin.MoreBossLoot", "MoreBossLoot", "1.1.0")]
    public class MoreBossLoot : BaseUnityPlugin
    {
        private Harmony harmony;

        private void Start()
        {
            harmony = new Harmony("tw.vocolboy.plugin.MoreBossLoot");
            harmony.PatchAll();
        }

        private void OnDestroy()
        {
            harmony.UnpatchSelf();
        }
    }

    [HarmonyPatch(typeof(OcEm), "deathActEnd")]
    public class Patch
    {
        private static readonly ManualLogSource Logger = BepInEx.Logging.Logger.CreateLogSource("MoreBossLoot");

        static void Postfix(OcEm __instance)
        {
            OcItemDropper dropper = Traverse.Create(__instance).Field("_ItemDropper").GetValue<OcItemDropper>();

            if (!(bool) dropper)
            {
                return;
            }

            bool isBoss = Traverse.Create(__instance).Property("SoEm").Property("IsBoss").GetValue<bool>();
            bool isMidBoss = Traverse.Create(__instance).Property("SoEm").Property("IsMidBoss").GetValue<bool>();


            Logger.LogInfo(string.Format("Is Boss {0}, Is Mid Boss {1}", isBoss, isMidBoss));

            if (isBoss || isMidBoss)
            {
                Logger.LogInfo(string.Format("Kill Boss {0}",
                    Traverse.Create(__instance).Field("_CurLv_SoEmData")
                        .Property("Name")
                        .GetValue<string>())
                );

                OcNetMng inst = SingletonMonoBehaviour<OcNetMng>.Inst;
                int[] accountIds = Traverse.Create(inst).Field("_ClientAccountId").GetValue<int[]>();
                int onlinePlayerCount = 1;

                //old version self will set true, after 4.21 version self set false
                for (int index = 0; index < 8; ++index)
                {
                    if (accountIds[index] != 0)
                        ++onlinePlayerCount;
                }

                Logger.LogInfo(string.Format("OnlinePlayer {0}, should drop {1} loot", onlinePlayerCount,
                    onlinePlayerCount));

                for (int i = 1; i < onlinePlayerCount; i++)
                {
                    Traverse.Create(dropper).Method("doKilledDropItem").GetValue();
                }
            }
        }
    }
}