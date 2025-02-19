using System;
using System.Collections.Generic;
using LTX.ChanneledProperties;
using Script.FightingPlan;
using Sirenix.OdinInspector;
using Spine;
using Spine.Unity;
using UnityEngine;

namespace Legendhair.Player.Animation
{
    public class Zombie_AnimationController : MonoBehaviour
    {
        private SkeletonAnimation _skeletonAnimation;

        private PrioritisedProperty<ZombieAnimationParameters> currentAnimationState;
        [SerializeField, ReadOnly] private string currentAnimation; // juste pour les logs
        
        [SerializeField] private ZombieAnimationParameters Idle;
        [SerializeField] private ZombieAnimationParameters Attack;
        
        private PreciousWord preciousWord;
        private string _wordName;

        private void Awake()
        {
            _skeletonAnimation = GetComponentInChildren<SkeletonAnimation>();
            preciousWord = GetComponent<PreciousWord>();

            if (!_skeletonAnimation) 
                return;
            
            currentAnimationState = new PrioritisedProperty<ZombieAnimationParameters>(Idle);
            currentAnimationState.AddOnValueChangeCallback(OnCurrentAnimationChanged);
        }

        private void Start()
        {
            _wordName = preciousWord.WordData.name.Split('_', StringSplitOptions.None)[0].ToUpper();
        }

        private void OnCurrentAnimationChanged(ZombieAnimationParameters parameters)
        {
            _skeletonAnimation.AnimationState.SetAnimation(0, _wordName + parameters.AssetName, parameters.Loop);
            _skeletonAnimation.timeScale = parameters.TimeScale;
            _skeletonAnimation.UnscaledTime = parameters.UnscaledTime;
            currentAnimation = _skeletonAnimation.AnimationState.ToString();
            if (_skeletonAnimation.skeleton != null)
            {
                _skeletonAnimation.skeleton.ScaleX = parameters.Flip ? -1 : 1;
            }
        }

        private void OnEnable()
        {
            if(!_skeletonAnimation)
                _skeletonAnimation = GetComponentInChildren<SkeletonAnimation>();
            
            if(_skeletonAnimation)
            {
                preciousWord.OnAttack += OnAttack;
                _skeletonAnimation.AnimationState.Complete += ProcessAnimationEnd;
            }
        }

        private void OnDisable()
        {
            UnsubscribeToEvents();
        }

        private void UnsubscribeToEvents()
        {
            if(_skeletonAnimation)
            {
                preciousWord.OnAttack -= OnAttack;
                _skeletonAnimation.AnimationState.Complete -= ProcessAnimationEnd;
                _skeletonAnimation = null;
            }
        }

        private void ProcessAnimationEnd(TrackEntry trackentry)
        {
            if (currentAnimationState == null || currentAnimationState.Value.Loop || !trackentry.IsComplete)
                return;

            currentAnimationState.RemovePriority(this);

            if (trackentry.Animation.Name == (_wordName + Attack.AssetName))
            {
                preciousWord.EndAttack();
            }
        }

        private void OnAttack()
        {
            StartAnimation(Attack);
        }

        private void StartAnimation(ZombieAnimationParameters parameters)
        {
            currentAnimationState.AddPriority(this, parameters.Priority, parameters);
        }

        private void StopAnimation(ZombieAnimationParameters parameters)
        {
            currentAnimationState.RemovePriority(this);
        }
    }
}
