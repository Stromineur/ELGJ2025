using LTX.ChanneledProperties.Priorities;
using NecroMotMicon.Script.FightingPlan;
using Sirenix.OdinInspector;
using Spine;
using Spine.Unity;
using UnityEngine;

namespace NecroMotMicon.Script.Animation.Words
{
    public abstract class WordAnimationController : MonoBehaviour
    {
        public SkeletonAnimation skeletonAnimation;

        protected Priority<WordAnimationParameters> currentAnimationState;
        [SerializeField, ReadOnly] private string currentAnimation; // juste pour les logs
        
        [SerializeField] protected WordAnimationParameters Idle;
        [SerializeField] protected WordAnimationParameters Attack;
        
        protected FightingWord word;
        private string _wordName;

        protected virtual void Awake()
        {
            skeletonAnimation = GetComponentInChildren<SkeletonAnimation>();
            word = GetComponent<FightingWord>();

            Init();
        }

        public void Init()
        {
            if (!skeletonAnimation) 
                return;
            
            currentAnimationState = new Priority<WordAnimationParameters>(Idle);
            currentAnimationState.AddOnValueChangeCallback(OnCurrentAnimationChanged);
        }

        private void Start()
        {
            _wordName = GetWordName();
        }

        protected abstract string GetWordName();

        private void OnCurrentAnimationChanged(WordAnimationParameters parameters)
        {
            skeletonAnimation.AnimationState.SetAnimation(0, _wordName + parameters.AssetName, parameters.Loop);
            skeletonAnimation.timeScale = parameters.TimeScale;
            skeletonAnimation.UnscaledTime = parameters.UnscaledTime;
            currentAnimation = skeletonAnimation.AnimationState.ToString();
            if (skeletonAnimation.skeleton != null)
            {
                skeletonAnimation.skeleton.ScaleX = parameters.Flip ? -1 : 1;
            }
        }

        protected virtual void OnEnable()
        {
            if(!skeletonAnimation)
                skeletonAnimation = GetComponentInChildren<SkeletonAnimation>();
            
            if(skeletonAnimation)
            {
                word.OnAttack += OnAttack;
                skeletonAnimation.AnimationState.Complete += ProcessAnimationEnd;
                //_skeletonAnimation.AnimationState.Event += ProcessAnimationEnd;
            }
        }

        protected virtual void OnDisable()
        {
            UnsubscribeToEvents();
        }

        private void UnsubscribeToEvents()
        {
            if(skeletonAnimation)
            {
                word.OnAttack -= OnAttack;
                skeletonAnimation.AnimationState.Complete -= ProcessAnimationEnd;
                skeletonAnimation = null;
            }
        }

        private void ProcessAnimationEnd(TrackEntry trackentry)
        {
            if (currentAnimationState == null || currentAnimationState.Value.Loop || !trackentry.IsComplete)
                return;

            currentAnimationState.RemovePriority(currentAnimationState.Value);

            if (trackentry.Animation.Name == (_wordName + Attack.AssetName))
            {
                word.EndAttack();
            }
        }

        private void OnAttack()
        {
            StartAnimation(Attack);
        }

        public void OnIdle()
        {
            StartAnimation(Idle);
        }

        internal void StartAnimation(WordAnimationParameters parameters)
        {
            currentAnimationState.AddPriority(parameters, parameters.Priority, parameters);
        }

        internal void Clear()
        {
            currentAnimationState.Clear();
        }

        internal void StopAttackAnimation()
        {
            StopAnimation(Attack);
        }

        internal void StopAnimation(WordAnimationParameters parameters)
        {
            currentAnimationState.RemovePriority(this);
        }
    }
}
