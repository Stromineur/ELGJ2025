using System;
using NecroMotMicon.Script.FightingPlan.WordBehaviour.Triggers;
using Spine;
using UnityEngine;
using Event = Spine.Event;

namespace NecroMotMicon.Script.Animation.Words
{
    public class WordEffectAnimationController : EffectAnimationController
    {
        public override event Action OnEffectTrigger;
        
        [SerializeField] private WordAnimationController _controller;
        [SerializeField] protected WordAnimationParameters effect;
        [SerializeField] protected string triggerName;

        protected virtual void Awake()
        {
            if (_controller == null)
                _controller = GetComponentInParent<WordAnimationController>();
            if (effectTrigger == null)
                effectTrigger = GetComponentInParent<EffectTrigger>();
        }

        protected virtual void OnEnable()
        {
            base.OnEnable();
            
            if (_controller != null && _controller.skeletonAnimation != null)
                _controller.skeletonAnimation.AnimationState.Event += HandleEvent;
        }

        protected virtual void OnDisable()
        {
            base.OnDisable();
            
            if (_controller != null && _controller.skeletonAnimation != null)
                _controller.skeletonAnimation.AnimationState.Event += HandleEvent;
        }

        protected override void InternalPlayAnimation()
        {
            _controller.StartAnimation(effect);
        }

        protected virtual void HandleEvent(TrackEntry trackEntry, Event e)
        {
            HandleEvent(e.ToString());
        }

        protected void HandleEvent(string eventName)
        {
            if (eventName == triggerName)
            {
                OnEffectTrigger?.Invoke();
            }
        }
    }
}
