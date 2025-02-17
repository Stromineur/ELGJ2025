using UnityEngine;

namespace Script.FightingPlan
{
    public class FightingLane : MonoBehaviour
    {
        [SerializeField] private Transform allyPosition;
        [SerializeField] private Transform enemyPosition;
        
        public void Spawn(FightingWord fightingWord, bool ally)
        {
            FightingWord word = Instantiate(fightingWord, ally ? allyPosition : enemyPosition);
        }
    }
}
