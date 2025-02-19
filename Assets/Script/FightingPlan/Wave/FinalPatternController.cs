using System.Collections.Generic;
using UnityEngine;

namespace Script.FightingPlan.Wave
{
    public class FinalPatternController : PatternController
    {
        private List<FightingWord> _badWords = new();
        
        protected override void HandleDelay()
        {
            if (!IsCurrentEnemyLast)
            {
                base.HandleDelay();
            }
        }

        protected override FightingWord SpawnEnemy()
        {
            FightingWord fightingWord = base.SpawnEnemy();

            if (fightingWord == null)
                return null;
            
            _badWords.Add(fightingWord);
            fightingWord.OnDeath += OnEnemyDeath;
            fightingWord.OnReachedEndEvent += OnEnemyDeath;
            return fightingWord;
        }

        private void OnEnemyDeath(FightingWord killed)
        {
            killed.OnDeath -= OnEnemyDeath;
            killed.OnReachedEndEvent -= OnEnemyDeath;
            _badWords.Remove(killed);
            
            if (_nbEnemies <= 0 && _badWords.Count <= 0)
            {
                EndPattern();
            }
        }

        private void OnEnemyDeath(FightingWord killed, FightingWord killer)
        {
            OnEnemyDeath(killed);
        }
    }
}
