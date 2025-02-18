using System;
using DG.Tweening;
using LTX.ChanneledProperties;
using Script.Core;
using Script.Words;
using UnityEngine;

namespace Script.FightingPlan
{
    public class PreciousWord : FightingWord
    {
        public WordData WordData => _wordData;

        [SerializeField] private float damage;
        private WordData _wordData;
        public bool IsInitialized { get; private set; }

        private float _remainingExhumingTime;

        protected override void InternalInit(IFightingData fightingData)
        {
            _wordData = fightingData as WordData;

            if(_wordData == null)
                return;
            
            damage = _wordData.BaseDamage;
            ShouldMove = false;
            IsInitialized = false;
            
            _remainingExhumingTime = _wordData.ExhumingTime * GameController.GameMetrics.ExhumingMultiplier;
            transform.localScale = Vector3.zero;
            transform.DOScale(Vector3.one, _wordData.ExhumingTime);
        }

        protected override void Update()
        {
            if (!IsInitialized)
            {
                _remainingExhumingTime -= Time.deltaTime;

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
        }

        protected override void Fight()
        {
            base.Fight();
            
            LastEnemySeen.Damage(this, damage);
            Die(LastEnemySeen);
        }

        protected override void InternalDamage(FightingWord initiator, float dmg)
        {
            Die(initiator);
        }

        private void OnBecameInvisible()
        {
            Die(this);
        }
    }
}
