using System;
using UnityEngine;

namespace Script.FightingPlan.WordBehaviour
{
    public class PeculeEffect : MonoBehaviour
    {
        [SerializeField] private float exhumingTimeToReduce = 40;
        
        private PreciousWord _preciousWord;

        private void Awake()
        {
            _preciousWord = GetComponentInParent<PreciousWord>();
        }

        private void OnEnable()
        {
            _preciousWord.OnDeath += ReduceExhumingTime;
        }

        private void OnDisable()
        {
            _preciousWord.OnDeath -= ReduceExhumingTime;
        }

        private void ReduceExhumingTime(FightingWord arg1, FightingWord arg2)
        {
            LaneManager.Instance.AddExhumingTime(-exhumingTimeToReduce);
        }
    }
}
