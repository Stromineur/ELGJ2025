using DG.Tweening;
using Script.Words;
using UnityEngine;

namespace Script.FightingPlan
{
    public class BadWord : FightingWord
    {
        public BadWordData BadWordData => _badWordData;
        public float Hp => hp;
        
        private BadWordData _badWordData;
        private float hp;
        Vector3 localScale;

        protected override void InternalInit(IFightingData fightingData, bool exhuming)
        {
            _badWordData = fightingData as BadWordData;
            
            if(!_badWordData)
                return;
            
            hp = _badWordData.Hp;
            _speed = -fightingData.Speed;
            
            ShouldMove = true;

            localScale = transform.localScale;
        }

        protected override void InternalDamage(FightingWord initiator, float dmg)
        {
            hp -= dmg;

            if (hp <= 0)
            {
                Die(initiator);
            }
            else
            {
                DOTween.Sequence()
                    .Append(transform.DOScale(localScale * 0.6f, 0.15f))
                    .Append(transform.DOScale(localScale, 0.15f));
            }
        }

        public override void ResetSlow()
        {
            _speed = -_badWordData.Speed;
        }
    }
}
