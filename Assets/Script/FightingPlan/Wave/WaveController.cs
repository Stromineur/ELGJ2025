using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace NecroMotMicon.Script.FightingPlan.Wave
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
        private List<BadWord> _badWords = new();

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
                return;

            PatternData patternData = _waveData.PatternOrder switch
            {
                PatternOrder.Random => _patternDatas[Random.Range(0, _patternDatas.Count)],
                PatternOrder.Order => _patternDatas[0],
                _ => _patternDatas[0]
            };
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

        public void AddBadWord(BadWord badWord)
        {
            _badWords.Add(badWord);
            badWord.OnDeath += RemoveBadWord;
        }

        private void RemoveBadWord(FightingWord killed, FightingWord _)
        {
            _badWords.Remove(killed as BadWord);
            
            if(AreAllBadWordsDone())
                TryEndWave();
        }

        public bool AreAllBadWordsDone()
        {
            return _badWords.Count == 0;
        }

        public void EndPattern()
        {
            StartNextPattern();
        }

        private void TryEndWave()
        {
            if(IsCurrentPatternLastPattern)
                EndWave();
        }

        private void EndWave()
        {
            WaveManager.TryEndWave();
        }
    }
}
