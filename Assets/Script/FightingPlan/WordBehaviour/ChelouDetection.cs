using NecroMotMicon.Script.Animation.Words;
using NecroMotMicon.Script.FightingPlan.WordBehaviour.Triggers;
using UnityEngine;

namespace NecroMotMicon.Script.FightingPlan.WordBehaviour
{
    public class ChelouDetection : EffectTrigger
    {
        private BadWord _badWord;
        private LineChanger _lineChanger;
        private int _changeLineRemaining;
        [SerializeField] private WordEffectAnimationController effectAnimationController;

        private void Awake()
        {
            _badWord = GetComponentInParent<BadWord>();
            _lineChanger = GetComponentInChildren<LineChanger>();
            _changeLineRemaining = 1;
            if(effectAnimationController == null)
                effectAnimationController = GetComponentInChildren<WordEffectAnimationController>();
        }

        private void OnEnable()
        {
            effectAnimationController.OnEffectTrigger += _lineChanger.ChangeLine;
        }

        private void OnDisable()
        {
            effectAnimationController.OnEffectTrigger -= _lineChanger.ChangeLine;
        }

        private void Update()
        {
            if (_changeLineRemaining <= 0)
                return;

            RaycastHit2D enemy = Physics2D.Raycast(transform.position, new Vector2(1, 0), _badWord.GetRaycastDistance() * 3f, _badWord.EnemyMask);

            if (enemy)
            {
                Trigger();
                _changeLineRemaining--;
            }
        }

        protected override void Setup() { }
    }
}
