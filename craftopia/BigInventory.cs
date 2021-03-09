using BepInEx;
using HarmonyLib;
using Oc.Item;
using Oc.Item.UI;
using Oc.Missions;

namespace BigInventory
{
    [BepInPlugin("tw.vocolboy.plugin.BigInventory", "BigInventory", "1.0.0")]
    public class BigInventory : BaseUnityPlugin
    {
        private void Start()
        {
            new Harmony("tw.vocolboy.plugin.BigInventory").PatchAll();
        }
    }

    [HarmonyPatch(typeof(OcMissionManager), "GameStartIncreaseInventory")]
    class PatchInventory
    {
        public static void Postfix()
        {
            ItemType[] bePatch = new ItemType[4] {ItemType.Equipment, ItemType.Consumption, ItemType.Building, ItemType.Material};
            
            foreach (var key in bePatch)
            {
                SingletonMonoBehaviour<OcItemUI_InventoryMng>.Inst.IncreaseListSize(key, 40);
            }
        }
    }
}