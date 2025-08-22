namespace NecroMotMicon.Script.FightingPlan.WordBehaviour.Triggers
{
    public class DeathEffectTrigger : EffectTrigger
    {
        protected override void Setup()
        {
            SubscribeToEvents();
        }
        
        private void OnEnable()
        {
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            if(isSetup)
                return;
            if(!fightingWord)
                return;
            
            fightingWord.OnDeath += Trigger;
            isSetup = true;
        }

        private void OnDisable()
        {
            if(!fightingWord)
                return;
            
            fightingWord.OnInitialized -= Trigger;
            isSetup = false;
        }

        private void Trigger(FightingWord arg1, FightingWord arg2)
        {
            Trigger();
        }
    }
}
