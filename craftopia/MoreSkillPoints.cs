using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using Oc;

namespace MoreSkillPoints
{
    [BepInPlugin("tw.vocolboy.plugin.MoreSkillPoints", "MoreSkillPoints", "1.1.0")]
    public class MoreSkillPoints : BaseUnityPlugin
    {
        private ConfigEntry<int> _increasePoint;

        void Awake()
        {
            _increasePoint = Config.Bind("config", "IncreasePoint", 2, "Increase Skill Point By Level Up");
        }

        private void Start()
        {
            Traverse.Create(typeof(OcDefine)).Field("INCREASE_SKILLPOINT_BY_LEVEL_UP").SetValue(_increasePoint.Value);
        }

        private void OnDestroy()
        {
            Traverse.Create(typeof(OcDefine)).Field("INCREASE_SKILLPOINT_BY_LEVEL_UP").SetValue(1);
        }
    }
}