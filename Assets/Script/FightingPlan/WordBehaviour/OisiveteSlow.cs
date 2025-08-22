using UnityEngine;

namespace NecroMotMicon.Script.FightingPlan.WordBehaviour
{
    public class OisiveteSlow : WordBehaviour
    {
        [SerializeField] private float slowMultiplier = 0.5f;
        [SerializeField] private float slowDuration = 5f;
        
        private PreciousWord _preciousWord;
        private BadWord _badWord;

        private void Awake()
        {
            _preciousWord = GetComponentInParent<PreciousWord>();
        }

        public override void Trigger()
        {
            
        }

        private void Start()
        {
            transform.SetParent(null);
        }

        private void OnEnable()
        {
            _preciousWord.OnHit += StartSlow;
        }

        private void OnDisable()
        {
            _preciousWord.OnHit -= StartSlow;
        }

        private void StartSlow(float f, FightingWord badWord)
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
