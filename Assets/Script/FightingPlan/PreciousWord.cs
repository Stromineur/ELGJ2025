using System;
using DG.Tweening;
using LTX.ChanneledProperties;
using Script.Words;
using UnityEngine;

namespace Script.FightingPlan
{
    public class PreciousWord : FightingWord
    {
        [SerializeField] private float damage;

        protected override void InternalInit(IFightingData fightingData)
        {
            WordData wordData = fightingData as WordData;
            
            if(wordData == null)
                return;
            
            damage = wordData.BaseDamage;
            ShouldMove = false;
            IsInitialized = false;
            
            Invoke(nameof(StartMoving), wordData.ExhumingTime);
            transform.localScale = Vector3.zero;
            transform.DOScale(Vector3.one, wordData.ExhumingTime);
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

        public override void Damage(FightingWord initiator, float dmg)
        {
            Die(initiator);
        }

        private void OnBecameInvisible()
        {
            Die(this);
        }
    }
}
