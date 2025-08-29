using System;
using LucidFactory.UI.Panels;
using NecroMotMicon.Script.FightingPlan.Wave;
using Script.Core;
using UnityEngine;

namespace NecroMotMicon.Script.FightingPlan.UI
{
    public class StartNextWaveButton : MonoBehaviour
    {
        [SerializeField] private LF_TabManager tabManager;

        private void Awake()
        {
            if (tabManager == null)
                tabManager = GetComponentInParent<LF_TabManager>();
        }

        private void OnEnable()
        {
            ServiceLocator.Instance.WaveManager.OnWaveEnd += ActivatePanel;
            ServiceLocator.Instance.WaveManager.OnWaveStarts += DeactivatePanel;
        }
        
        private void OnDisable()
        {
            ServiceLocator.Instance.WaveManager.OnWaveEnd -= ActivatePanel;
            ServiceLocator.Instance.WaveManager.OnWaveStarts -= DeactivatePanel;
        }

        private void ActivatePanel(WaveData obj)
        {
            tabManager.OpenTab(name);
        }

        private void DeactivatePanel(int obj)
        {
            DeactivatePanel();
        }

        private void DeactivatePanel()
        {
            tabManager.OpenTab("HUD");
        }

        public void EndInterWave()
        {
            ServiceLocator.Instance.WaveManager.StartNextWave();
        }
    }
}
