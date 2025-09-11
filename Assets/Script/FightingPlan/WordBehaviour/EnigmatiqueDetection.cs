using System;
using NecroMotMicon.Script.Animation.Words;
using NecroMotMicon.Script.FightingPlan.WordBehaviour.Triggers;
using UnityEngine;

namespace NecroMotMicon.Script.FightingPlan.WordBehaviour
{
    public class EnigmatiqueDetection : EffectTrigger
    {
        public FightingLane MoveToLane { get; private set; }

        [SerializeField] private WordEffectAnimationController effectAnimationController;
        
        private PreciousWord _preciousWord;
        [SerializeField] private LineChanger _lineChanger;
        private float _timeBeforeNextChangeLine;
        
        private float _distance;
        private float _closestEnemy;

        private void Awake()
        {
            _preciousWord = GetComponentInParent<PreciousWord>();
            if(_lineChanger == null)
                _lineChanger = GetComponentInChildren<LineChanger>();
            if(effectAnimationController == null)
                effectAnimationController = GetComponentInChildren<WordEffectAnimationController>();
            _timeBeforeNextChangeLine = 0;
        }

        private void OnEnable()
        {
            effectAnimationController.OnEffectTrigger += ChangeLine;
        }

        private void OnDisable()
        {
            effectAnimationController.OnEffectTrigger -= ChangeLine;
        }

        private void Update()
        {
            if (!_preciousWord.IsInitialized)
                return;
            
            if (_timeBeforeNextChangeLine > 0)
            {
                _timeBeforeNextChangeLine -= Time.deltaTime;
                return;
            }

            _closestEnemy = Mathf.Infinity;
            MoveToLane = null;
            
            foreach (BadWord badWord in _preciousWord.FightingLane.BadWords)
            {
                if (!IsEnemyCloser(badWord)) 
                    continue;
                
                _closestEnemy = _distance;
            }

            FightingLane previousLane = _preciousWord.FightingLane.TopLane;
            if (previousLane)
            {
                foreach (BadWord badWord in previousLane.BadWords)
                {
                    if (!IsEnemyCloser(badWord)) 
                        continue;
                    
                    MoveToLane = previousLane;
                    _closestEnemy = _distance;
                }
            }

            FightingLane nextLane = _preciousWord.FightingLane.BottomLane;
            if (nextLane)
            {
                foreach (BadWord badWord in nextLane.BadWords)
                {
                    if (!IsEnemyCloser(badWord)) 
                        continue;
                    
                    MoveToLane = nextLane;
                    _closestEnemy = _distance;
                }
            }

            if (MoveToLane)
            {
                Trigger();
                _timeBeforeNextChangeLine = 2f;
            }
        }

        public void ChangeLine()
        {
            _lineChanger.ChangeLine(MoveToLane);
        }

        private bool IsEnemyCloser(BadWord badWord)
        {
            if (badWord && badWord.transform.position.x > transform.position.x)
            {
                _distance = badWord.transform.position.x - transform.position.x;
                if (_distance < _closestEnemy)
                {
                    return true;
                }
            }
            return false;
        }

        protected override void Setup() { }
    }
}
