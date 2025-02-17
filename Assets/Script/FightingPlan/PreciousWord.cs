using UnityEngine;

namespace Script.FightingPlan
{
    public class PreciousWord : FightingWord
    {
        [SerializeField] private float damage;
        
        protected override void Fight()
        {
            base.Fight();
            
            LastEnemySeen.Damage(damage);
            Die();
        }

        public override void Damage(float dmg = 0)
        {
            Die();
        }

        private void Die()
        {
            Destroy(gameObject);
        }
    }
}
