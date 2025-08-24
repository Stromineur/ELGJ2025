using System;
using System.Linq;
using DG.Tweening;
using NecroMotMicon.Script.Words;
using Script.Core;
using UnityEngine;

namespace NecroMotMicon.Script.FightingPlan
{
    public class PreciousWord : FightingWord
    {
        [SerializeField] private BoxCollider2D _boxCollider2D;
        
        public WordData WordData => _wordData;

        [SerializeField] private float damage;
        private WordData _wordData;
        public bool IsInitialized { get; private set; }

        private float _maxExhumingTime;
        private float _remainingExhumingTime;

        public override event Action OnInitialized;

        private void Awake()
        {
            _boxCollider2D = GetComponent<BoxCollider2D>();
            _boxCollider2D.enabled = false;
        }

        protected override void InternalInit(IFightingData fightingData, bool exhuming = true)
        {
            _wordData = fightingData as WordData;

            if(_wordData == null)
                return;

            if (exhuming && LaneManager.Instance.TryUseFreeExhuming())
                exhuming = false;
            damage = _wordData.BaseDamage;
            ShouldMove = false;
            IsInitialized = false;
            
            if(exhuming)
            {
                _remainingExhumingTime = _wordData.ExhumingTime * GameController.GameMetrics.ExhumingMultiplier;
                _maxExhumingTime = _remainingExhumingTime;
                Vector3 currentScale = transform.localScale;
                transform.localScale = Vector3.zero;
                transform.DOScale(currentScale, _wordData.ExhumingTime * GameController.GameMetrics.ExhumingMultiplier);
            }
            else
            {
                _remainingExhumingTime = 0.5f;
                _maxExhumingTime = _remainingExhumingTime;
                Vector3 currentScale = transform.localScale;
                transform.localScale = Vector3.zero;
                transform.DOScale(currentScale, _maxExhumingTime - 0.1f);
                OnInitialized?.Invoke();
            }
        }

        protected override void Update()
        {
            if (!IsInitialized)
            {
                _remainingExhumingTime -= Time.deltaTime;
                FightingLane.UpdateExhumingBar(_maxExhumingTime - _remainingExhumingTime, _maxExhumingTime);

                if (_remainingExhumingTime <= 0)
                {
                    StartMoving();
                }
            }
            else 
                base.Update();
        }

        protected override float GetRaycastDistance()
        {
            return base.GetRaycastDistance() * 1.7f;
        }

        public void AddExhumingTime(float time)
        {
            _remainingExhumingTime += time;
        }

        private void StartMoving()
        {
            IsInitialized = true;
            ShouldMove = true;
            localScale = transform.localScale;
            _boxCollider2D.enabled = true;
            OnInitialized?.Invoke();
        }

        protected override void Fight()
        {
            base.Fight();

            float dmg = damage;

            if (LastEnemySeen is BadWord badWord)
            {
                bool strongAgainstEnemy = _wordData.StrongAgainst.Contains(badWord.BadWordData);
                if(strongAgainstEnemy)
                    dmg *= 2;

                badWord.Damage(this, dmg);
                if(strongAgainstEnemy && badWord.Hp <= 0)
                    LaneManager.Instance.AddFreeExhuming();
                Damage(LastEnemySeen, badWord.BadWordData.Damage);
            }
        }

        public override void EndAttack()
        {
            base.EndAttack();
            //Die(LastEnemySeen);
        }

        public override void ResetSlow()
        {
            _speed = _wordData.Speed;
        }

        private void OnBecameInvisible()
        {
            OnReachedEnd();
        }
    }
}
