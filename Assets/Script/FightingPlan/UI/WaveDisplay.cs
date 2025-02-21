using Script.FightingPlan.Wave;
using TMPro;
using UnityEngine;

namespace Script.FightingPlan.UI
{
    public class WaveDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text waveText;
        [SerializeField] private WaveManager waveManager;

        private void OnEnable()
        {
            waveManager.OnWaveStarts += UpdateWaveDisplay;
        }

        private void OnDisable()
        {
            waveManager.OnWaveStarts -= UpdateWaveDisplay;
        }

        private void UpdateWaveDisplay(int obj)
        {
            waveText.text = "Vague " + obj;
        }
    }
}
