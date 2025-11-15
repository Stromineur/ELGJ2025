using System;
using LucidFactory.UI.Panels;
using NecroMotMicon.Script.FightingPlan.Wave;
using Script.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NecroMotMicon.Script.FightingPlan.UI
{
    public class WinPanelDisplay : MonoBehaviour
    {
        private LF_TabManager tabManager;

        private void Awake()
        {
            if (tabManager == null)
                tabManager = GetComponentInParent<LF_TabManager>();
        }

        private void OnEnable()
        {
            ServiceLocator.Instance.WaveManager.OnWin += OnWin;
        }

        private void OnDisable()
        {
            if (ServiceLocator.Instance.WaveManager != null)
                ServiceLocator.Instance.WaveManager.OnWin -= OnWin;
        }

        private void OnWin()
        {
            tabManager.OpenTab(name);
            Time.timeScale = 0;
        }

        public void Restart()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("Main_Scene");
        }
        
        public void Quit()
        {
            Application.Quit();
        }
    }
}
