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

        protected override void InternalInit(IFightingData fightingData)
        {
            _badWordData = fightingData as BadWordData;
            
            if(!_badWordData)
                return;
            
            hp = _badWordData.Hp;
            _speed = -fightingData.Speed;
            
            ShouldMove = true;
        }

        protected override void InternalDamage(FightingWord initiator, float dmg)
        {
            hp -= dmg;

            if (hp <= 0)
            {
                Die(initiator);
            }
        }

        public override void ResetSlow()
        {
            _speed = -_badWordData.Speed;
        }
    }
}
