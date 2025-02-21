using System;
using System.Linq;
using DG.Tweening;
using LTX.ChanneledProperties;
using Script.Core;
using Script.Words;
using UnityEngine;

namespace Script.FightingPlan
{
    public class PreciousWord : FightingWord
    {
        public event Action OnInitialized;
        public WordData WordData => _wordData;

        [SerializeField] private float damage;
        private WordData _wordData;
        public bool IsInitialized { get; private set; }

        private float _maxExhumingTime;
        private float _remainingExhumingTime;

        protected override void InternalInit(IFightingData fightingData, bool exhuming = true)
        {
            _wordData = fightingData as WordData;

            if(_wordData == null)
                return;
            
            damage = _wordData.BaseDamage;
            ShouldMove = !exhuming;
            IsInitialized = !exhuming;
            
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

        public void AddExhumingTime(float time)
        {
            _remainingExhumingTime += time;
        }

        private void StartMoving()
        {
            IsInitialized = true;
            ShouldMove = true;
            OnInitialized?.Invoke();
        }

        protected override void Fight()
        {
            base.Fight();

            float dmg = damage;

            if (LastEnemySeen is BadWord badWord && _wordData.StrongAgainst.Contains(badWord.BadWordData))
                dmg = 9999;
            
            LastEnemySeen.Damage(this, dmg);
        }

        protected override void InternalDamage(FightingWord initiator, float dmg)
        {
            Die(initiator);
        }

        public override void EndAttack()
        {
            base.EndAttack();
            Die(LastEnemySeen);
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
