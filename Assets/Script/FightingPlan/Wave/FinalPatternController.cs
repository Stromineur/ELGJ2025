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
            _badWords.Add(fightingWord);
            fightingWord.OnDeath += OnEnemyDeath;
            return fightingWord;
        }

        private void OnEnemyDeath(FightingWord killed, FightingWord killer)
        {
            killed.OnDeath -= OnEnemyDeath;
            _badWords.Remove(killed);
            
            if (_nbEnemies <= 0 && _badWords.Count <= 0)
            {
                EndPattern();
            }
        }
    }
}
