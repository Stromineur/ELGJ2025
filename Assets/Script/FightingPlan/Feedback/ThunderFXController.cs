using NecroMotMicon.Script.Animation;
using NecroMotMicon.Script.Animation.Words;
using NecroMotMicon.Script.FightingPlan.WordBehaviour.Triggers;
using Spine;
using Spine.Unity;
using UnityEngine;

namespace NecroMotMicon.Script.FightingPlan.Feedback
{
    public class ThunderFXController : MonoBehaviour
    {
        [SerializeField] private EffectAnimationController effectAnimationController;
        [SerializeField] private PreciousWordAnimationController animationController;
        [SerializeField] private EffectTrigger effectTrigger;
        [SerializeField] private ParticleSystem particleSystemPrefab;
        [SerializeField] private ParticleSystem pSystem;
        [SerializeField] private SkeletonAnimation skeletonAnimation;
        
        private void OnEnable()
        {
            effectAnimationController.OnEffectTrigger += PlayThunderAnimation;
            effectTrigger.OnTriggerEffect += PlayFX;
        }

        private void OnDisable()
        {
            effectAnimationController.OnEffectTrigger -= PlayThunderAnimation;
            effectTrigger.OnTriggerEffect -= PlayFX;
            skeletonAnimation.AnimationState.Complete -= ProcessAnimationEnd;
        }

        private void PlayFX()
        {
            pSystem =  Instantiate(particleSystemPrefab, transform.position, transform.rotation, transform);
            pSystem.Play();
        }

        public void PlayThunderAnimation()
        {
            skeletonAnimation.gameObject.SetActive(true);
            skeletonAnimation.AnimationState.Complete += ProcessAnimationEnd;
        }

        private void ProcessAnimationEnd(TrackEntry trackEntry)
        {
            skeletonAnimation.AnimationState.Complete -= ProcessAnimationEnd;
            skeletonAnimation.gameObject.SetActive(false);
            pSystem.Stop();
        }
    }
}
