using System;
using System.Linq;
using DG.Tweening;
using NecroMotMicon.Script.Words;
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

        protected override float GetSpeed()
        {
            return base.GetSpeed() * FightingLane.LaneSpeed.Value;
        }

        protected override float GetLanePosition(FightingLane fightingLane)
        {
            return fightingLane.EnemyPosition.transform.position.y;
        }

        public override void ResetSlow()
        {
            _speed = -_badWordData.Speed;
        }
        
        public override void Die(FightingWord killer)
        {
            base.Die(killer);
            
            if(killer.FightingData is WordData wordData && wordData.StrongAgainst.Contains(BadWordData))
                LaneManager.Instance.AddFreeExhuming();
        }
    }
}
