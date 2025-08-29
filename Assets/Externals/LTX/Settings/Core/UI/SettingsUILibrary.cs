using UnityEngine;

namespace LTX.Settings.UI
{
    [CreateAssetMenu(menuName = "LTX/Settings/UILibrary")]
    public class SettingsUILibrary : ScriptableObject
    {
        [System.Serializable]
        public struct SettingsUIPrefab
        {
            public SettingType type;
            public GameObject prefab;
        }

        public GameObject sectionPrefab;
        public GameObject categoryPrefab;
        public GameObject tabPrefab;
        public GameObject tabButtonPrefab;

        public SettingsUIPrefab[] settingsUI;
    }


    
}
