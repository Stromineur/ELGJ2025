using System;
using UnityEngine;

namespace NecroMotMicon.Script.FightingPlan.WordBehaviour.Feel
{
    [RequireComponent(typeof(LaneDamageBehaviour))]
    public class LaneDamageFeelController : MonoBehaviour
    {
        private LaneDamageBehaviour _laneDamageBehaviour;
        [SerializeField] private ParticleSystem particleSystem;

        private void Awake()
        {
            _laneDamageBehaviour =  GetComponent<LaneDamageBehaviour>();
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
            if(!particleSystem.isPlaying)
                particleSystem.Play();
        }
    }
}
