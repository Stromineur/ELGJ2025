using UnityEngine;

namespace Script.FightingPlan.Wave
{
    public class PatternController : MonoBehaviour
    {
        private PatternData _patternData;
        private WaveController _waveController;

        private int _nbEnemies;

        public void Init(PatternData patternData, WaveController waveController)
        {
            _patternData = patternData;
            _waveController = waveController;
            _nbEnemies = Random.Range(_patternData.MinNumber, _patternData.MaxNumber + 1);
        }

        public void StartPattern()
        {
            SpawnNextEnemy();
        }

        private void HandleDelay()
        {
            if (_nbEnemies <= 0)
            {
                Invoke(nameof(EndPattern), _patternData.DelayAfter);
                return;
            }
            
            Invoke(nameof(SpawnNextEnemy), _patternData.DelayBetween);
        }

        private void SpawnNextEnemy()
        {
            _waveController.WaveManager.SpawnEnemy(_patternData.BadWord, transform);
            _nbEnemies--;
            
            HandleDelay();
        }

        private void EndPattern()
        {
            _waveController.EndPattern();
        }
    }
}
