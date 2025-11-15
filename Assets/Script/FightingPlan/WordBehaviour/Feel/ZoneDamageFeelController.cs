using System;
using UnityEngine;

namespace NecroMotMicon.Script.FightingPlan.WordBehaviour.Feel
{
    [RequireComponent(typeof(ZoneDamageBehaviour))]
    public class ZoneDamageFeelController : MonoBehaviour
    {
        private ZoneDamageBehaviour _zoneDamageBehaviour;
        [SerializeField] private ParticleSystem _particleSystem;

        private void Awake()
        {
            _zoneDamageBehaviour = GetComponent<ZoneDamageBehaviour>();
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
            _particleSystem.Play();
        }
    }
}
