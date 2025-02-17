using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Script.FightingPlan.Wave
{
    public class WaveManager : MonoBehaviour
    {
        [SerializeField] private FightingLane[] fightingLanes;
        [SerializeField] private WaveData[] waves;
        private int _currentWave;
        private int _currentWaveStep;
        private List<WaveController> _waveControllers = new();

        [Button]
        public void StartGame()
        {
            StartNextWave();
        }
        
        private void StartNextWave()
        {
            if (_currentWave >= waves.Length)
            {
                EndGame();
                return;
            }
            
            WaveData waveData = waves[_currentWave];

            WaveController waveController = 
                new GameObject($"WaveController{_currentWave}")
                    .AddComponent<WaveController>();
            waveController.transform.SetParent(transform);
            _waveControllers.Add(waveController);

            waveController.Init(waveData, this);
            waveController.StartWave();

            _currentWave++;
        }

        public void SpawnEnemy(BadWordData badWordData, Transform parent)
        {
            fightingLanes[Random.Range(0, 5)].Spawn(badWordData, parent);
        }
        
        public void EndWave()
        {
            StartNextWave();
        }
        
        public void EndGame()
        {
            Debug.Log("Fin de partie");
        }
    }
}
