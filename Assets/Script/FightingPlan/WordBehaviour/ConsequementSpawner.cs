namespace Script.FightingPlan.WordBehaviour
{
    public class ConsequementSpawner : WordSpawner
    {
        protected override void OnDeath(FightingWord killed, FightingWord killer)
        {
            if (killer is BadWord badWord)
            {
                if(!badWord.BadWordData.name.Contains("Ducoup"))
                    return;
            }
            
            base.OnDeath(killed, killer);
        }
    }
}
