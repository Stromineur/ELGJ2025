using System;
using UnityEngine;

namespace NecroMotMicon.Script.FightingPlan.WordBehaviour.Feel
{
    [RequireComponent(typeof(ZoneDamageBehaviour))]
    public class ZoneDamageFeelController : MonoBehaviour
    {
        private static readonly int Play = Animator.StringToHash("PlayFeel");
        private ZoneDamageBehaviour _zoneDamageBehaviour;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Animator _animator;

        private void Awake()
        {
            _zoneDamageBehaviour = GetComponent<ZoneDamageBehaviour>();
            transform.localScale = Vector3.one * _zoneDamageBehaviour.Range / _zoneDamageBehaviour.FightingWord.transform.localScale.y;
        }

        private void OnEnable()
        {
            _zoneDamageBehaviour.OnTriggered += PlayFeel;
        }

        private void OnDisable()
        {
            _zoneDamageBehaviour.OnTriggered -= PlayFeel;
        }

        private void PlayFeel()
        {
            _animator.SetTrigger(Play);
        }
    }
}
