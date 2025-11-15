using LTX.ChanneledProperties.Priorities;
using NecroMotMicon.Script.Animation.Words.Precious;
using UnityEngine;

namespace NecroMotMicon.Script.FightingPlan.WordBehaviour
{
    public class OisiveteSlow : WordBehaviour
    {
        [SerializeField] private float slowMultiplier = 0.5f;
        [SerializeField] private OisiveteAnimationController oisiveteAnimationController;
        
        private PreciousWord _preciousWord;

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
            _preciousWord.OnInitialized += StartSlow;
            _preciousWord.OnDeath += StopSlow;
        }

        private void OnDisable()
        {
            _preciousWord.OnInitialized -= StartSlow;
            _preciousWord.OnDeath -= StopSlow;
        }

        private void StartSlow()
        {
            oisiveteAnimationController.PlaySlowAnimation();
            _preciousWord.FightingLane.LaneSpeed.AddPriority(this, PriorityTags.Default, slowMultiplier);
        }

        private void StopSlow(FightingWord arg1, FightingWord arg2)
        {
            _preciousWord.FightingLane.LaneSpeed.RemovePriority(this);
        }
    }
}
