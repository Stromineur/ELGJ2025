using System;
using System.Collections.Generic;
using UnityEngine;

namespace NecroMotMicon.Script.FightingPlan.WordBehaviour
{
    public class LaneDamageBehaviour : WordBehaviour
    {
        public event Action OnTriggered;
        
        public float Range => _range;
        public float Damage => _damage;
        
        [SerializeField] private float _range = 3;
        [SerializeField] private float _damage = 100;
        
        public override void Trigger()
        {
            List<FightingWord> fightingWords = new List<FightingWord>(fightingWord.FightingLane.FightingWords);
            foreach (FightingWord word in fightingWords)
            {
                if(!word)
                    continue;
                
                if (word.GetType() == fightingWord.GetType()) 
                    continue;

                if (!(Mathf.Abs(word.transform.position.x - transform.position.x) < _range)) 
                    continue;
                
                word.Damage(fightingWord, _damage);
            }
            OnTriggered?.Invoke();
        }
    }
}
