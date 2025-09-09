using System;
using NecroMotMicon.Script.Animation.Words;
using NecroMotMicon.Script.FightingPlan.WordBehaviour.Triggers;
using UnityEngine;

namespace NecroMotMicon.Script.FightingPlan.WordBehaviour
{
    public abstract class WordBehaviour : MonoBehaviour
    {
        [SerializeField] protected EffectTrigger effectTrigger;
        [SerializeField] protected EffectAnimationController effectAnimationController;

        public FightingWord FightingWord => fightingWord;
        protected FightingWord fightingWord;
        
        protected virtual void Awake()
        {
            fightingWord = GetComponentInParent<FightingWord>();
            if(effectTrigger == null)  
                effectTrigger = gameObject.GetComponentInParent<EffectTrigger>();
            
            effectTrigger.Setup(fightingWord);
        }

        private void OnEnable()
        {
            effectAnimationController.OnEffectTrigger += Trigger;
        }

        private void OnDisable()
        {
            effectAnimationController.OnEffectTrigger -= Trigger;
        }

        public abstract void Trigger();
    }
}
