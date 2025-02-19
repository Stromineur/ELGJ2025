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
    public class Necromocien_AnimationController : MonoBehaviour
    {
        private SkeletonAnimation _skeletonAnimation;
        [SerializeField] private List<FightingLane> fightingLanes;

        private PrioritisedProperty<PlayerAnimationParameters> currentAnimationState;
        [SerializeField, ReadOnly] private string currentAnimation; // juste pour les logs
        
        [SerializeField] private PlayerAnimationParameters Idle;
        [SerializeField] private PlayerAnimationParameters Attack;

        private void Awake()
        {
            _skeletonAnimation = GetComponentInChildren<SkeletonAnimation>();

            if (!_skeletonAnimation) 
                return;

            /*
            Idle = Resources.Load<PlayerAnimationParameters>("a");
            Attack = Resources.Load<PlayerAnimationParameters>("AnimationsParameters/Necromocien/Attack");
            */
            
            currentAnimationState = new PrioritisedProperty<PlayerAnimationParameters>(Idle);
            currentAnimationState.AddOnValueChangeCallback(OnCurrentAnimationChanged, true);
        }

        private void OnCurrentAnimationChanged(PlayerAnimationParameters parameters)
        {
            _skeletonAnimation.AnimationState.SetAnimation(0, parameters.Asset, parameters.Loop);
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
            foreach (FightingLane fightingLane in fightingLanes)
            {
                fightingLane.OnSpawnPreciousWord += OnAttack;
            }
            
            if(!_skeletonAnimation)
                _skeletonAnimation = GetComponentInChildren<SkeletonAnimation>();
            
            if(_skeletonAnimation)
            {
                //ajouter l'event
                _skeletonAnimation.AnimationState.Complete += ProcessAnimationEnd;
            }
        }

        private void OnDisable()
        {
            UnsubscribeToEvents();
        }

        private void UnsubscribeToEvents()
        {
            foreach (FightingLane fightingLane in fightingLanes)
            {
                fightingLane.OnSpawnPreciousWord -= OnAttack;
            }
            
            if(_skeletonAnimation)
            {
                _skeletonAnimation.AnimationState.Complete -= ProcessAnimationEnd;
                _skeletonAnimation = null;
            }
        }

        private void ProcessAnimationEnd(TrackEntry trackentry)
        {
            if (currentAnimationState == null || currentAnimationState.Value.Loop || !trackentry.IsComplete || currentAnimationState.Value.Asset.name != trackentry.Animation.Name)
                return;

            currentAnimationState.RemovePriority(currentAnimationState.Value.Asset);
        }

        private void OnAttack()
        {
            StartAnimation(Attack);
        }

        private void StartAnimation(PlayerAnimationParameters parameters)
        {
            currentAnimationState.AddPriority(parameters.Asset, parameters.Priority, parameters);
        }

        private void StopAnimation(PlayerAnimationParameters parameters)
        {
            currentAnimationState.RemovePriority(parameters.Asset);
        }
    }
}
