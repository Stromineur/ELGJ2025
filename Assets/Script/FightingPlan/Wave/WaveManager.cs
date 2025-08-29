using System;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace NecroMotMicon.Script.FightingPlan.Wave
{
    public class WaveManager : MonoBehaviour
    {
        public event Action<int> OnWaveStarts;
        public event Action<WaveData> OnWaveEnd;
        public event Action OnWin;
        
        public WaveController CurrentWave => _waveControllers[^1];
        
        [SerializeField] public FightingLane[] fightingLanes;
        private List<float> lanesOdd = new();
        [SerializeField] private WaveData[] waves;
        private int _currentWave;
        private int _currentWaveStep;
        private List<WaveController> _waveControllers = new();

        public float Timer { get; private set; } = 3f;

        public void StartTimer()
        {
            DOTween.To(() => Timer, x => Timer = x, 0, Timer)
                .SetEase(Ease.Linear)
                .OnComplete(StartGame);
        }

        [Button]
        public void StartGame()
        {
            StartNextWave();
        }
        
        public void StartNextWave()
        {
            
            
            WaveData waveData = waves[_currentWave];

            WaveController waveController = 
                new GameObject($"WaveController{_currentWave}")
                    .AddComponent<WaveController>();
            waveController.transform.SetParent(transform);
            _waveControllers.Add(waveController);

            waveController.Init(waveData, this);
            waveController.StartWave();

            _currentWave++;
            
            OnWaveStarts?.Invoke(_currentWave);
        }

        public FightingWord SpawnEnemy(BadWordData badWordData, Transform parent)
        {
            float overallOdds = 0;
            lanesOdd.Clear();
            foreach (FightingLane fightingLane in fightingLanes)
            {
                float enemySpawnOdds = fightingLane.GetEnemySpawnOdds();
                lanesOdd.Add(enemySpawnOdds);
                overallOdds += enemySpawnOdds;
            }
            float rnd = Random.Range(0, overallOdds);

            int laneNumber = 0;
            for (int i = 0; i < lanesOdd.Count; i++)
            {
                float odd = lanesOdd[i];
                if (rnd <= odd)
                {
                    laneNumber = i;
                    break;
                }
                rnd -= odd;
            }

            return fightingLanes[laneNumber].Spawn(badWordData, parent);
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
        
        public void TryEndWave()
        {
            Invoke(nameof(EndWave), 0.5f);
        }

        public void EndWave()
        {
            foreach (FightingLane fightingLane in fightingLanes)
            {
                fightingLane.CleanUpWave();
                if (fightingLane.BadWords.Count >= 1)
                {
                    TryEndWave();
                    return;
                }
            }
            
            if (_currentWave >= waves.Length)
            {
                EndGame();
                return;
            }
            
            WaveData waveData = waves[_currentWave];
            OnWaveEnd?.Invoke(waveData);
        }
        
        public void EndGame()
        {
            OnWin?.Invoke();
        }
        
        #if UNITY_EDITOR

        [Button]
        public void FillLanes()
        {
            fightingLanes = FindObjectsByType<FightingLane>(FindObjectsInactive.Exclude, FindObjectsSortMode.InstanceID);
            Array.Sort(fightingLanes,(a,b) => a.name.CompareTo(b.name));
        }
        
        #endif
    }
}
