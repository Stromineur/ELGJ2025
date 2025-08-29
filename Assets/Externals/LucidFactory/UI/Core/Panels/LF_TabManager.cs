using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace LucidFactory.UI.Panels
{
    [AddComponentMenu( "LucidFactory/UI/TabManager" )]
    public class LF_TabManager : MonoBehaviour
    {
#if UNITY_EDITOR
        [FormerlySerializedAs("showPanel")]
        [SerializeField, BoxGroup("Editor"), PropertyRange(0, "@tabs.Count - 1"), OnValueChanged(nameof(OnTabChange), InvokeOnInitialize = false)]
        [DisableInPlayMode]
        private int showTab = 0;
#endif
        
        [SerializeField, BoxGroup("Settings"), PropertyRange(0, "@tabs.Count - 1")] 
        protected int startTab = 0;
        [FormerlySerializedAs("tab")] [SerializeField, BoxGroup("Settings")]
        private List<LF_Tab> tabs;

        
        [BoxGroup("PanelManager"), ShowInInspector] 
        private int currentTabIndex;

        protected int CurrentTabIndex
        {
            get
            {
                return currentTabIndex;
            }
            private set
            {
                if (value != CurrentTabIndex)
                {
                    currentTabIndex = value;
                    OnTabChange(value);
                }
                
            }
        }

        public string CurrentTab
        {
            get
            {
                return tabs[currentTabIndex].name;
            }
            private set
            {
                int index = tabs.FindIndex(ctx => ctx.name == value);
                if (index != -1)
                    CurrentTabIndex = index;
            }
        }

        private void OnTabChange(int index)
        {
            for (int i = 0; i < tabs.Count; i++)
            {
                var panel = tabs[i];
                
                if (i == index)
                {
                    panel.Open();
                }
                else
                {
                    panel.Close();
                }
            }
        }

        public void OpenTab(string tab)
        {
            currentTabIndex = -1;
            CurrentTab = tab;
        }

        public void OpenTab(int tab)
        {
            currentTabIndex = -1;
            CurrentTabIndex = tab;
        }

        public void CloseAllTabs()
        {
            CurrentTabIndex = -1;
        }
        protected virtual void Awake()
        {
            currentTabIndex = startTab;
            OnTabChange(startTab);
        }
    }
}
