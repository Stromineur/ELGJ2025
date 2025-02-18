using System;
using Legendhair.Utilities;
using UnityEngine;

namespace Script.FightingPlan
{
    public class PlayerArea : MonoBehaviour
    {
        public event Action<float> OnDamageTaken;

        public float Hp => hp;
        
        [SerializeField] private LayerMask enemyMask;
        private float hp;

        private void Awake()
        {
            hp = 1000;
            OnDamageTaken?.Invoke(hp);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (LayerMaskUtilities.IsMaskContainedIn(other.gameObject.layer, enemyMask))
            {
                BadWord badWord = other.gameObject.GetComponent<BadWord>();
                hp -= badWord.BadWordData.Damage;
                OnDamageTaken?.Invoke(hp);
                badWord.Die(badWord);
            }
        }
    }
}
