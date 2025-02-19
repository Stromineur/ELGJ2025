using System;
using UnityEngine;

namespace Script.FightingPlan.WordBehaviour
{
    public class OisiveteSlow : MonoBehaviour
    {
        [SerializeField] private float slowMultiplier = 0.5f;
        [SerializeField] private float slowDuration = 5f;
        
        private PreciousWord _preciousWord;
        private BadWord _badWord;

        private void Awake()
        {
            _preciousWord = GetComponentInParent<PreciousWord>();
        }

        private void Start()
        {
            transform.SetParent(null);
        }

        private void OnEnable()
        {
            _preciousWord.OnDeath += StartSlow;
        }

        private void OnDisable()
        {
            _preciousWord.OnDeath -= StartSlow;
        }

        private void StartSlow(FightingWord killed, FightingWord badWord)
        {
            _badWord = badWord as BadWord;
            badWord.Slow(slowMultiplier);
            Invoke(nameof(StopSlow), slowDuration);
        }

        private void StopSlow()
        {
            if(_badWord)
                _badWord.ResetSlow();
            
            Destroy(gameObject);
        }
    }
}
