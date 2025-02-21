using Script.FightingPlan.Wave;
using TMPro;
using UnityEngine;

namespace Script.FightingPlan.UI
{
    public class TimerDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text timer;
        [SerializeField] private WaveManager waveManager;

        private void Update()
        {
            timer.text = Mathf.CeilToInt(waveManager.Timer).ToString();
            
            if(waveManager.Timer <= 0)
                Destroy(gameObject);
        }
    }
}
