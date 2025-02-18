using System;
using DG.Tweening;
using Script.Words;
using UnityEngine;

namespace Script.FightingPlan
{
    public abstract class FightingWord : MonoBehaviour
    {
        // 1er FightingWord est l'objet sur lequel est le script, le "tué", 2ème Fighting word est le tueur
        public event Action<FightingWord, FightingWord> OnDeath;
        public event Action OnSpawn;
        public event Action<float, FightingWord> OnHit;
        
        [SerializeField] private LayerMask _enemyMask;
        
        [SerializeField] protected float _speed;
        protected bool IsInitialized;
        protected bool ShouldMove;
        protected FightingWord LastEnemySeen;
        public FightingLane FightingLane { get; private set; }

        public LayerMask EnemyMask => _enemyMask;

        public float Speed => _speed;

        public void Init(IFightingData fightingData, FightingLane fightingLane)
        {
            _speed = fightingData.Speed;
            FightingLane = fightingLane;
            
            InternalInit(fightingData);
            OnSpawn?.Invoke();
        }

        public void MoveLane(FightingLane fightingLane)
        {
            transform.DOMoveX(fightingLane.transform.position.x, 0.5f);
            FightingLane = fightingLane;
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
            enemy = Physics2D.Raycast(transform.position, new Vector2(0, 1), Mathf.Sign(Speed) + Speed * Time.deltaTime, EnemyMask);
            return enemy;
        }

        protected virtual void Fight()
        {
            
        }

        public void Damage(FightingWord initiator, float dmg)
        {
            OnHit?.Invoke(dmg, initiator);
            InternalDamage(initiator, dmg);
        }

        protected abstract void InternalDamage(FightingWord initiator, float dmg);

        public void Die(FightingWord killer)
        {
            OnDeath?.Invoke(this, killer ? killer : this);
            Destroy(gameObject);
        }
    }
}
