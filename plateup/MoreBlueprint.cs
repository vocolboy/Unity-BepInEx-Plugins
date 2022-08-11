using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using Kitchen;

namespace PlateUpMoreBlueprint
{
    [BepInPlugin("tw.vocolboy.plugin.MoreBlueprint","MoreBlueprint","1.0.0")]
    public class MoreBlueprint: BaseUnityPlugin
    {
        private void Start()
        {
            new Harmony("tw.vocolboy.plugin.MoreBlueprint").PatchAll();
        }
    }

    [HarmonyPatch(typeof(DifficultyHelpers), "TotalShopCount")]
    class PatchBlueprint
    {
        static void Postfix(ref int __result)
        {
            __result = 10;
        }
    }
}
