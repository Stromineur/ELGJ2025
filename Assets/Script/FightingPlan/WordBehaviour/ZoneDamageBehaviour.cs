using System;
using System.Collections.Generic;
using UnityEngine;

namespace NecroMotMicon.Script.FightingPlan.WordBehaviour
{
    public class ZoneDamageBehaviour : WordBehaviour
    {
        public event Action OnTriggered;
        
        public float Range => _range;
        public float Damage => _damage;
        
        [SerializeField] private float _range = 3;
        [SerializeField] private float _damage = 100;
        
        public override void Trigger()
        {
            List<FightingWord> fightingWords = new List<FightingWord>(fightingWord.FightingLane.FightingWords);
            if(fightingWord.FightingLane.BottomLane is { } rightLane)
                fightingWords.AddRange(rightLane.FightingWords);
            if(fightingWord.FightingLane.TopLane is { } leftLane)
                fightingWords.AddRange(leftLane.FightingWords);
            
            foreach (FightingWord word in fightingWords)
            {
                if(!word)
                    continue;
                
                if (word.GetType() == fightingWord.GetType()) 
                    continue;

                if (!(Vector2.Distance(word.transform.position, fightingWord.transform.position) < _range)) 
                    continue;
                
                word.Damage(fightingWord, _damage);
            }
            OnTriggered?.Invoke();
        }
    }
}
