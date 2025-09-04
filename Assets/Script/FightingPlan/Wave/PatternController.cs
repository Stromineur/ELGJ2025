using System.Collections.Generic;
using UnityEngine;

namespace NecroMotMicon.Script.FightingPlan.Wave
{
    public class PatternController : MonoBehaviour
    {
        public bool IsCurrentEnemyLast => _nbEnemies <= 0;
        
        protected PatternData _patternData;
        protected WaveController _waveController;

        protected int _nbEnemies;
        protected List<FightingWord> _badWords = new();

        public virtual void Init(PatternData patternData, WaveController waveController)
        {
            _patternData = patternData;
            _waveController = waveController;
            _nbEnemies = Random.Range(_patternData.MinNumber, _patternData.MaxNumber + 1);
        }

        public void StartPattern()
        {
            SpawnNextEnemy();
        }

        protected virtual void HandleDelay()
        {
            if (IsCurrentEnemyLast)
            {
                if(_waveController.IsCurrentPatternLastPattern)
                    EndPattern();
                else 
                    Invoke(nameof(EndPattern), _patternData.DelayAfter * _waveController.WaveData.DelayAfterMultiplier);
                return;
            }
            
            Invoke(nameof(SpawnNextEnemy), _patternData.DelayBetween * _waveController.WaveData.DelayBetweenMultiplier);
        }

        protected virtual void SpawnNextEnemy()
        {
            SpawnEnemy();
            _nbEnemies--;
            
            HandleDelay();
        }

        protected virtual FightingWord SpawnEnemy()
        {
            FightingWord fightingWord = _waveController.WaveManager.SpawnEnemy(_patternData.BadWord, transform);
            if (fightingWord == null)
                return null;
            
            _badWords.Add(fightingWord);
            _waveController.AddBadWord(fightingWord as BadWord);
            fightingWord.OnDeath += OnEnemyDeath;
            fightingWord.OnReachedEndEvent += OnEnemyDeath;
            return fightingWord;
        }
        
        protected virtual void OnEnemyDeath(FightingWord killed)
        {
            killed.OnDeath -= OnEnemyDeath;
            killed.OnReachedEndEvent -= OnEnemyDeath;
            _badWords.Remove(killed);
        }

        private void OnEnemyDeath(FightingWord killed, FightingWord killer)
        {
            OnEnemyDeath(killed);
        }

        protected void EndPattern()
        {
            _waveController.EndPattern();
        }
    }
}
