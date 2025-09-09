using System;
using LTX.ChanneledProperties.Priorities;
using NecroMotMicon.Script.FightingPlan;
using Sirenix.OdinInspector;
using Spine;
using Spine.Unity;
using UnityEngine;

namespace NecroMotMicon.Script.Animation
{
    public class Zombie_AnimationController : MonoBehaviour
    {
        private SkeletonAnimation _skeletonAnimation;

        private Priority<WordAnimationParameters> currentAnimationState;
        [SerializeField, ReadOnly] private string currentAnimation; // juste pour les logs
        
        [SerializeField] private WordAnimationParameters Idle;
        [SerializeField] private WordAnimationParameters Attack;
        
        private PreciousWord preciousWord;
        private string _wordName;

        private void Awake()
        {
            _skeletonAnimation = GetComponentInChildren<SkeletonAnimation>();
            preciousWord = GetComponent<PreciousWord>();

            if (!_skeletonAnimation) 
                return;
            
            currentAnimationState = new Priority<WordAnimationParameters>(Idle);
            currentAnimationState.AddOnValueChangeCallback(OnCurrentAnimationChanged);
        }

        private void Start()
        {
            _wordName = preciousWord.WordData.name.Split("Zombie_", StringSplitOptions.None)[0].ToUpper();
        }

        private void OnCurrentAnimationChanged(WordAnimationParameters parameters)
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

        private void StartAnimation(WordAnimationParameters parameters)
        {
            currentAnimationState.AddPriority(this, parameters.Priority, parameters);
        }

        private void StopAnimation(WordAnimationParameters parameters)
        {
            currentAnimationState.RemovePriority(this);
        }
    }
}
