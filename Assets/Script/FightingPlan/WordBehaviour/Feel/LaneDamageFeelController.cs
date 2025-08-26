using System;
using UnityEngine;

namespace NecroMotMicon.Script.FightingPlan.WordBehaviour.Feel
{
    [RequireComponent(typeof(LaneDamageBehaviour))]
    public class LaneDamageFeelController : MonoBehaviour
    {
        private static readonly int Play = Animator.StringToHash("PlayFeel");
        private LaneDamageBehaviour _laneDamageBehaviour;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Animator _animator;

        private void Awake()
        {
            _laneDamageBehaviour =  GetComponent<LaneDamageBehaviour>();
            transform.localScale = new Vector3(_laneDamageBehaviour.Range / _laneDamageBehaviour.FightingWord.transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }

        private void OnEnable()
        {
            _laneDamageBehaviour.OnTriggered += PlayFeel;
        }

        private void OnDisable()
        {
            _laneDamageBehaviour.OnTriggered -= PlayFeel;
        }

        private void PlayFeel()
        {
            _animator.SetTrigger(Play);
        }
    }
}
