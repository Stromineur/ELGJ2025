using System;
using NecroMotMicon.Script.FightingPlan.WordBehaviour.Triggers;
using UnityEngine;

namespace NecroMotMicon.Script.Animation.Words
{
    public abstract class EffectAnimationController : MonoBehaviour
    {
        public abstract event Action OnEffectTrigger;
        
        [SerializeField] protected EffectTrigger effectTrigger;

        protected virtual void OnEnable()
        {
            if(effectTrigger == null)
                return;
            
            effectTrigger.OnTriggerEffect += PlayAnimation;
        }

        protected virtual void OnDisable()
        {
            if(effectTrigger == null)
                return;
            
            effectTrigger.OnTriggerEffect += PlayAnimation;
        }

        private void PlayAnimation()
        {
            Invoke(nameof(InternalPlayAnimation), 0.05f);
        }

        protected abstract void InternalPlayAnimation();
    }
}
