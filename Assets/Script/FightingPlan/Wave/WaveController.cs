using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Script.FightingPlan.Wave
{
    public class WaveController : MonoBehaviour
    {
        public WaveData WaveData => _waveData;
        public WaveManager WaveManager => _waveManager;
        public bool IsCurrentPatternLastPattern => _nbPattern == 0;
        
        private WaveManager _waveManager;
        private WaveData _waveData;
        private List<PatternData> _patternDatas = new();
        private int _nbPattern;
        private List<PatternController> _patternControllers = new();

        public void Init(WaveData waveData, WaveManager waveManager)
        {
            _waveData = waveData;
            _waveManager = waveManager;
            
            _patternDatas = _waveData.WavePatterns.ToList();
            _nbPattern = _waveData.NbPattern;
        }

        public void StartWave()
        {
            StartNextPattern();
        }
        
        private void StartNextPattern()
        {
            if (_nbPattern <= 0)
            {
                EndWave();
                return;
            }
            
            PatternData patternData = _patternDatas[Random.Range(0, _patternDatas.Count)];
            _patternDatas.Remove(patternData);

            PatternController patternController = SpawnPattern();
            patternController.transform.SetParent(transform);
            _patternControllers.Add(patternController);

            _nbPattern--;

            patternController.Init(patternData, this);
            patternController.StartPattern();
        }

        private PatternController SpawnPattern()
        {
            PatternController patternController;
            GameObject patternObject = new($"PatternController{_waveData.NbPattern - _nbPattern}");
            if (_nbPattern == 1)
            {
                patternObject.name = $"Final{patternObject.name}";
                patternController = patternObject.AddComponent<FinalPatternController>();
            }
            else 
                patternController = patternObject.AddComponent<PatternController>();

            return patternController;
        }

        public void EndPattern()
        {
            StartNextPattern();
        }

        private void EndWave()
        {
            WaveManager.EndWave();
        }
    }
}
