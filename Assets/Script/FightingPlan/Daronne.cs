namespace NecroMotMicon.Script.FightingPlan
{
    public class Daronne : BadWord
    {
        private FightingWord killer;
        
        public override void Die(FightingWord killer)
        {
            InvokePreDeath();
            this.killer = killer;
            Invoke(nameof(InvokedDie), 1);
        }

        private void InvokedDie()
        {
            base.Die(killer);
        }
    }
}
