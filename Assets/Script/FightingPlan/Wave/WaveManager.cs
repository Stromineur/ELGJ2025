using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Script.FightingPlan.Wave
{
    public class WaveManager : MonoBehaviour
    {
        public WaveController CurrentWave => _waveControllers[^1];
        
        [SerializeField] private FightingLane[] fightingLanes;
        [SerializeField] private WaveData[] waves;
        private int _currentWave;
        private int _currentWaveStep;
        private List<WaveController> _waveControllers = new();

#if ENABLE_RUNTIME_GI
        [Button]
        public void StartGame()
        {
            StartNextWave();
        }
#endif
        
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

        public FightingWord SpawnEnemy(BadWordData badWordData, Transform parent)
        {
            return fightingLanes[Random.Range(0, 5)].Spawn(badWordData, parent);
        }

        public FightingWord SpawnEnemy(BadWordData badWordData, Transform parent, int fightingLane)
        {
            return SpawnEnemy(badWordData, parent, fightingLanes[fightingLane]);
        }

        public FightingWord SpawnEnemy(BadWordData badWordData, Transform parent, int fightingLane, Vector2 position)
        {
            return SpawnEnemy(badWordData, parent, fightingLanes[fightingLane], position);
        }

        public FightingWord SpawnEnemy(BadWordData badWordData, Transform parent, FightingLane fightingLane)
        {
            return fightingLane.Spawn(badWordData, parent);
        }

        public FightingWord SpawnEnemy(BadWordData badWordData, Transform parent, FightingLane fightingLane, Vector2 position)
        {
            return fightingLane.Spawn(badWordData, parent, position);
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
