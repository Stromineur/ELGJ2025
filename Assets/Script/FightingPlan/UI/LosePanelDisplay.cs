using System;
using Script.FightingPlan.Wave;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script.FightingPlan.UI
{
    public class LosePanelDisplay : MonoBehaviour
    {
        [SerializeField] private PlayerArea playerArea;
        [SerializeField] private GameObject content;

        private void OnEnable()
        {
            playerArea.OnDamageTaken += OnDamageTaken;
        }

        private void OnDisable()
        {
            playerArea.OnDamageTaken -= OnDamageTaken;
        }

        private void OnDamageTaken(float f)
        {
            if (f > 0)
                return;
            
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
