using System;
using LucidFactory.UI.Panels;
using NecroMotMicon.Script.FightingPlan.Wave;
using Script.Core;
using TMPro;
using UnityEngine;

namespace NecroMotMicon.Script.FightingPlan.UI
{
    public class TimerDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text timer;
        [SerializeField] private WaveManager waveManager;
        [SerializeField] private LF_TabManager tabManager;

        private void Awake()
        {
            if (waveManager == null)
                waveManager = ServiceLocator.Instance.WaveManager;
        }

        private void Update()
        {
            timer.text = Mathf.CeilToInt(waveManager.Timer).ToString();
            
            if(waveManager.Timer <= 0)
            {
                tabManager.OpenTab("HUD");
                Destroy(gameObject);
            }
        }
    }
}
