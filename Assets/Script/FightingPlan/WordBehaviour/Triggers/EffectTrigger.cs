using System;
using UnityEngine;

namespace NecroMotMicon.Script.FightingPlan.WordBehaviour.Triggers
{
    public abstract class EffectTrigger : MonoBehaviour
    {
        public event Action OnTriggerEffect;
        
        protected FightingWord fightingWord;
        protected bool isSetup;

        public void Setup(FightingWord fightingWord)
        {
            this.fightingWord = fightingWord;
            
            Setup();
        }

        protected abstract void Setup();

        protected void Trigger()
        {
            OnTriggerEffect?.Invoke();
        }
    }
}
