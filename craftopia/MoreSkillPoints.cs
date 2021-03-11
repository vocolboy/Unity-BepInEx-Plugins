using BepInEx;
using HarmonyLib;
using Oc;

namespace MoreSkillPoints
{
    [BepInPlugin("tw.vocolboy.plugin.MoreSkillPoints", "MoreSkillPoints", "1.0.0")]
    public class MoreSkillPoints : BaseUnityPlugin
    {
        private void Start()
        {
            Traverse.Create(typeof(OcDefine)).Field("INCREASE_SKILLPOINT_BY_LEVEL_UP").SetValue(2);
        }

        private void OnDestroy()
        {
            Traverse.Create(typeof(OcDefine)).Field("INCREASE_SKILLPOINT_BY_LEVEL_UP").SetValue(1);
        }
    }
}