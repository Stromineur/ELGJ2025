using System;
using DG.Tweening;
using LTX.ChanneledProperties;
using Script.Words;
using UnityEngine;

namespace Script.FightingPlan
{
    public class PreciousWord : FightingWord
    {
        public WordData WordData => _wordData;

        [SerializeField] private float damage;
        private WordData _wordData;

        protected override void InternalInit(IFightingData fightingData)
        {
            _wordData = fightingData as WordData;

            if(_wordData == null)
                return;
            
            damage = _wordData.BaseDamage;
            ShouldMove = false;
            IsInitialized = false;
            
            Invoke(nameof(StartMoving), _wordData.ExhumingTime);
            transform.localScale = Vector3.zero;
            transform.DOScale(Vector3.one, _wordData.ExhumingTime);
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
