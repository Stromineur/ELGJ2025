using UnityEngine;

namespace NecroMotMicon.Script.FightingPlan.WordBehaviour.Triggers
{
    public abstract class EffectTrigger : MonoBehaviour
    {
        protected FightingWord fightingWord;
        protected WordBehaviour wordBehaviour;
        protected bool isSetup;

        public void Setup(FightingWord fightingWord, WordBehaviour wordBehaviour)
        {
            this.fightingWord = fightingWord;
            this.wordBehaviour = wordBehaviour;
            
            Setup();
        }

        protected abstract void Setup();

        protected void Trigger()
        {
            wordBehaviour.Trigger();
        }
    }
}
