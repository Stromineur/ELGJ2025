using System;
using DG.Tweening;
using UnityEngine;

namespace NecroMotMicon.Script.FightingPlan
{
    public class BadWord : FightingWord
    {
        public BadWordData BadWordData => _badWordData;
        
        private BadWordData _badWordData;

        public override event Action OnInitialized;

        protected override void InternalInit(IFightingData fightingData, bool exhuming)
        {
            _badWordData = fightingData as BadWordData;
            
            if(!_badWordData)
                return;
            
            _speed = -fightingData.Speed;
            
            ShouldMove = true;

            localScale = transform.localScale;
            OnInitialized?.Invoke();
        }

        protected override float GetLanePosition(FightingLane fightingLane)
        {
            return fightingLane.EnemyPosition.transform.position.y;
        }

        public override void ResetSlow()
        {
            _speed = -_badWordData.Speed;
        }
    }
}
