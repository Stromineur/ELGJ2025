namespace NecroMotMicon.Script.FightingPlan.WordBehaviour.Triggers
{
    public class SpawnEffectTrigger : EffectTrigger
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
            
            fightingWord.OnInitialized += Trigger;
            isSetup = true;
        }

        private void OnDisable()
        {
            if(!fightingWord)
                return;
            
            fightingWord.OnInitialized -= Trigger;
            isSetup = false;
        }
    }
}
