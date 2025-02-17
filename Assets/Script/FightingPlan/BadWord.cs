using Script.Words;
using UnityEngine;

namespace Script.FightingPlan
{
    public class BadWord : FightingWord
    {
        private float hp;

        protected override void InternalInit(IFightingData fightingData)
        {
            BadWordData badWordData = fightingData as BadWordData;
            
            if(!badWordData)
                return;
            
            hp = badWordData.Hp;
            Speed = -fightingData.Speed;
            
            IsInitialized = true;
            ShouldMove = true;
        }

        public override void Damage(float dmg)
        {
            hp -= dmg;

            if (hp <= 0)
            {
                Die();
            }
        }
    }
}
