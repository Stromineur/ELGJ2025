using System;
using Script.FightingPlan.Wave;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script.FightingPlan.UI
{
    public class WinPanelDisplay : MonoBehaviour
    {
        [SerializeField] private WaveManager _waveManager;
        [SerializeField] private GameObject content;

        private void OnEnable()
        {
            _waveManager.OnWin += OnWin;
        }

        private void OnDisable()
        {
            _waveManager.OnWin -= OnWin;
        }

        private void OnWin()
        {
            content.SetActive(true);
            Time.timeScale = 0;
        }

        public void Restart()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("Main_Scene");
        }
    }
}
