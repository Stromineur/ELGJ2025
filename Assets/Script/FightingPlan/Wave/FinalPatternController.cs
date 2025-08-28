using System.Collections.Generic;

namespace NecroMotMicon.Script.FightingPlan.Wave
{
    public class FinalPatternController : PatternController
    {
        protected override void HandleDelay()
        {
            if (!IsCurrentEnemyLast)
            {
                base.HandleDelay();
            }
        }

        protected override void OnEnemyDeath(FightingWord killed)
        {
            base.OnEnemyDeath(killed);
            
            if (_nbEnemies <= 0 && _badWords.Count <= 0)
            {
                EndPattern();
            }
        }
    }
}
