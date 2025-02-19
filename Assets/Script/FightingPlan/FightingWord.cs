using System;
using DG.Tweening;
using Script.Core;
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
        public event Action<FightingWord> OnReachedEndEvent;
        
        [SerializeField] private LayerMask _enemyMask;
        
        [SerializeField] protected float _speed;
        protected bool ShouldMove;
        protected FightingWord LastEnemySeen;
        public FightingLane FightingLane { get; private set; }

        public LayerMask EnemyMask => _enemyMask;

        public float Speed => _speed;

        private bool _isDead;

        public void Init(IFightingData fightingData, FightingLane fightingLane, bool exhuming = true)
        {
            _speed = fightingData.Speed;
            FightingLane = fightingLane;
            
            InternalInit(fightingData, exhuming);
            OnSpawn?.Invoke();
        }

        public void MoveLane(FightingLane fightingLane)
        {
            FightingLane.RemoveWordFromLane(this);
            transform.DOMoveX(fightingLane.transform.position.x, 0.5f);
            FightingLane = fightingLane;
            fightingLane.AddWordToLane(this);
        }
        
        protected abstract void InternalInit(IFightingData fightingData, bool exhuming = true);

        protected virtual void Update()
        {
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
            transform.position = new Vector2(transform.position.x, transform.position.y + Speed * Time.deltaTime * GameController.GameMetrics.SpeedMultiplier);
        }

        private bool IsEnemyHere(out RaycastHit2D enemy)
        {
            enemy = Physics2D.Raycast(transform.position, new Vector2(0, 1), Mathf.Sign(Speed) + Speed * GameController.GameMetrics.SpeedMultiplier * Time.deltaTime, EnemyMask);
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
            if(_isDead)
                return;

            _isDead = true;
            OnDeath?.Invoke(this, killer ? killer : this);
            Destroy(gameObject);
        }

        public void Slow(float multiplier)
        {
            _speed *= multiplier;
        }

        public abstract void ResetSlow();

        public void OnReachedEnd()
        {
            OnReachedEndEvent?.Invoke(this);
            
            Destroy(gameObject);
        }
    }
}
