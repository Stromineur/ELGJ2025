using NecroMotMicon.Script.FightingPlan.Wave;
using Script.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NecroMotMicon.Script.FightingPlan.UI
{
    public class WinPanelDisplay : MonoBehaviour
    {
        [SerializeField] private GameObject content;

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
