using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Script.FightingPlan.Wave
{
    public class WaveController : MonoBehaviour
    {
        public WaveManager WaveManager => _waveManager;
        
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

            PatternController patternController = 
                new GameObject($"PatternController{_waveData.NbPattern - _nbPattern}")
                    .AddComponent<PatternController>();
            patternController.transform.SetParent(transform);
            _patternControllers.Add(patternController);

            patternController.Init(patternData, this);
            patternController.StartPattern();

            _nbPattern--;
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
