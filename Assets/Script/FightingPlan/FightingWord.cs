using System;
using Script.Words;
using UnityEngine;

namespace Script.FightingPlan
{
    public abstract class FightingWord : MonoBehaviour
    {
        [SerializeField] private LayerMask EnemyMask;
        
        [SerializeField] protected float Speed;
        protected bool IsInitialized;
        protected bool ShouldMove;
        protected FightingWord LastEnemySeen;

        public void Init(IFightingData fightingData)
        {
            Speed = fightingData.Speed;
            
            InternalInit(fightingData);
        }
        
        protected abstract void InternalInit(IFightingData fightingData);

        private void Update()
        {
            if(!IsInitialized)
                return;
            
            ShouldMove = !IsEnemyHere(out RaycastHit2D hit);
            if (!ShouldMove)
            {
                LastEnemySeen = hit.transform.GetComponent<FightingWord>();
                Fight();
            }
            else
            {
                Move();
            }
        }

        private void Move()
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + Speed * Time.deltaTime);
        }

        private bool IsEnemyHere(out RaycastHit2D enemy)
        {
            enemy = Physics2D.Raycast(transform.position, new Vector2(0, 1), Speed * Time.deltaTime, EnemyMask);
            return enemy;
        }

        protected virtual void Fight()
        {
            
        }

        public abstract void Damage(float dmg = 0);

        protected void Die()
        {
            Destroy(gameObject);
        }
    }
}
