using System;
using DG.Tweening;
using Script.Core;
using UnityEngine;

namespace NecroMotMicon.Script.FightingPlan
{
    public abstract class FightingWord : MonoBehaviour
    {
        // 1er FightingWord est l'objet sur lequel est le script, le "tué", 2ème Fighting word est le tueur
        public event Action<FightingWord, FightingWord> OnDeath;
        public event Action OnSpawn;
        public abstract event Action OnInitialized;
        public event Action<float, FightingWord> OnHit;
        public event Action<float, float> OnHpChanged;
        public event Action<FightingWord> OnReachedEndEvent;
        public event Action OnAttack;
        public event Action OnAttackEnd;
        
        [SerializeField] private LayerMask _enemyMask;
        
        [SerializeField] protected float _speed;
        public float Hp => hp;
        [SerializeField] protected float hp;
        protected bool ShouldMove;
        protected FightingWord LastEnemySeen;
        public FightingLane FightingLane { get; private set; }
        private IFightingData _fightingData;
        protected Vector3 localScale;

        public LayerMask EnemyMask => _enemyMask;

        public float Speed => _speed;

        private bool _isDead;

        private float attackCd;

        public void Init(IFightingData fightingData, FightingLane fightingLane, bool exhuming = true)
        {
            hp = fightingData.Hp;
            _speed = fightingData.Speed;
            FightingLane = fightingLane;
            _fightingData = fightingData;
            
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
            attackCd -= Time.deltaTime;
            ShouldMove = !IsEnemyHere(out RaycastHit2D hit);
            if (!ShouldMove)
            {
                if(attackCd > 0)
                    return;

                attackCd = 1f;
                LastEnemySeen = hit.transform.GetComponent<FightingWord>();
                OnAttack?.Invoke();
                Invoke("Fight", 0.3f);
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
            enemy = Physics2D.Raycast(transform.position, new Vector2(0, 1), GetRaycastDistance(), EnemyMask);
            return enemy;
        }

        protected virtual float GetRaycastDistance()
        {
            return Mathf.Sign(Speed) + Speed * GameController.GameMetrics.SpeedMultiplier * Time.deltaTime;
        }

        protected virtual void Fight()
        {
            
        }

        public void Damage(FightingWord initiator, float dmg)
        {
            OnHit?.Invoke(dmg, initiator);
            InternalDamage(initiator, dmg);
            OnHpChanged?.Invoke(hp, _fightingData.Hp);
        }

        public virtual void EndAttack()
        {
            OnAttackEnd?.Invoke();
        }

        protected virtual void InternalDamage(FightingWord initiator, float dmg)
        {
            hp -= dmg;

            if (hp <= 0)
            {
                Die(initiator);
            }
            else
            {
                DOTween.Sequence()
                    .Append(transform.DOScale(localScale * 0.6f, 0.15f))
                    .Append(transform.DOScale(localScale, 0.15f));
            }
        }
        
        public void Die(FightingWord killer)
        {
            if(_isDead)
                return;

            _isDead = true;
            OnDeath?.Invoke(this, killer ? killer : this);
            transform.DOScale(0f, 0.2f).OnComplete(() => Destroy(gameObject));
        }

        public void Slow(float multiplier)
        {
            _speed *= multiplier;
        }

        public void SetSpeed(float speed)
        {
            _speed = speed;
        }

        public abstract void ResetSlow();

        public void OnReachedEnd()
        {
            OnReachedEndEvent?.Invoke(this);
            
            Destroy(gameObject);
        }
    }
}
