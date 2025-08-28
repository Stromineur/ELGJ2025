using System;
using NecroMotMicon.Script.FightingPlan.Wave;
using Script.Core;
using UnityEngine;

namespace NecroMotMicon.Script.FightingPlan.UI
{
    public class StartNextWaveButton : MonoBehaviour
    {
        [SerializeField] private GameObject panel;

        private void Awake()
        {
            DeactivatePanel();
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
            panel.SetActive(true);
        }

        private void DeactivatePanel(int obj)
        {
            DeactivatePanel();
        }

        private void DeactivatePanel()
        {
            panel.SetActive(false);
        }

        public void EndInterWave()
        {
            ServiceLocator.Instance.WaveManager.StartNextWave();
        }
    }
}
