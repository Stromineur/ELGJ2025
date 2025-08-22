using System;
using NecroMotMicon.Script.FightingPlan.WordBehaviour.Triggers;
using UnityEngine;

namespace NecroMotMicon.Script.FightingPlan.WordBehaviour
{
    public abstract class WordBehaviour : MonoBehaviour
    {
        [SerializeField] protected EffectTrigger effectTrigger;

        public FightingWord FightingWord => fightingWord;
        protected FightingWord fightingWord;
        
        protected virtual void Awake()
        {
            fightingWord = GetComponentInParent<FightingWord>();
            if(effectTrigger == null)  
                effectTrigger = gameObject.GetComponent<EffectTrigger>();
            
            effectTrigger.Setup(fightingWord, this);
        }

        public abstract void Trigger();
    }
}
