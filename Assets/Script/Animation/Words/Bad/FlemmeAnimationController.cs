using UnityEngine;

namespace NecroMotMicon.Script.Animation.Words.Bad
{
    public class FlemmeAnimationController : BadWordAnimationController
    {
        [SerializeField] protected WordAnimationParameters NeutralIdle;
        [SerializeField] protected WordAnimationParameters NeutralAttack;
        [SerializeField] protected WordAnimationParameters RageIdle;
        [SerializeField] protected WordAnimationParameters RageAttack;
        private WordEffectAnimationController _controller;

        protected override void Awake()
        {
            base.Awake();
            
            _controller = GetComponent<WordEffectAnimationController>();
            Idle = NeutralIdle;
            Attack = NeutralAttack;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            
            _controller.OnEffectTrigger += OnEffectTrigger;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            
            _controller.OnEffectTrigger -= OnEffectTrigger;
        }

        private void OnEffectTrigger()
        {
            Idle = RageIdle;
            Attack = RageAttack;
            StartAnimation(RageIdle);
        }
    }
}