using System;
using Legendhair.Utilities;
using Script.Core;
using UnityEngine;

namespace NecroMotMicon.Script.FightingPlan
{
    public class PlayerArea : MonoBehaviour
    {
        public event Action<float> OnDamageTaken;

        public float Hp => hp;
        
        [SerializeField] private LayerMask enemyMask;
        private float hp;

        private void Awake()
        {
            hp = GameController.GameMetrics.StartBookHp;
            OnDamageTaken?.Invoke(hp);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (LayerMaskUtilities.IsMaskContainedIn(other.gameObject.layer, enemyMask))
            {
                BadWord badWord = other.gameObject.GetComponent<BadWord>();
                hp -= badWord.BadWordData.BookDamage;
                OnDamageTaken?.Invoke(hp);
                badWord.Die(badWord);
            }
        }
    }
}
