using UnityEngine;

namespace NecroMotMicon.Script.FightingPlan.WordBehaviour
{
    public class FuryBehaviour : WordBehaviour
    {
        [SerializeField] private float newSpeed = 250;
        
        public override void Trigger()
        {
            fightingWord.SetSpeed(newSpeed);
        }
    }
}
