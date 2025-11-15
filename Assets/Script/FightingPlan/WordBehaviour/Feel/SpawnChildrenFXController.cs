using System;
using NecroMotMicon.Script.Animation.Words;
using NecroMotMicon.Script.FightingPlan.WordBehaviour.Triggers;
using UnityEngine;

namespace NecroMotMicon.Script.FightingPlan.WordBehaviour.Feel
{
    public class SpawnChildrenFXController : MonoBehaviour
    {
        [SerializeField] private EffectAnimationController effectAnimationController;
        [SerializeField] private ParticleSystem particleSystemPrefab;
        [SerializeField] private float delay;

        private void OnEnable()
        {
            effectAnimationController.OnEffectTrigger += InvokeFX;
        }

        private void OnDisable()
        {
            effectAnimationController.OnEffectTrigger -= InvokeFX;
        }

        private void InvokeFX()
        {
            Invoke(nameof(PlayFX), delay);
        }

        private void PlayFX()
        {
            ParticleSystem particle = Instantiate(particleSystemPrefab, transform.position, Quaternion.identity);
            particle.Play();
        }
    }
}
