using FMOD.Studio;
using FMODUnity;
using UnityEngine;

namespace NecroMotMicon.Script.FightingPlan.Sound.PreciousWordSpecific
{
    public class FaquinSoundController : SpecificWordSoundController
    {
        [SerializeField] private StudioEventEmitter aoeEvent;
        private EventInstance _aoeEventInstance;

        protected override void InternalOnEnable()
        {
            soundController.word.OnInitialized += PlayAoeSound;
            soundController.word.OnDeath += StopAoeSound;
        }

        protected override void InternalOnDisable()
        {
            soundController.word.OnInitialized -= PlayAoeSound;
            soundController.word.OnDeath -= StopAoeSound;
            StopAoeSound();
        }

        private void PlayAoeSound()
        {
            aoeEvent.Play();
        }

        private void StopAoeSound(FightingWord arg1, FightingWord arg2)
        {
            StopAoeSound();
        }

        private void StopAoeSound()
        {
            aoeEvent.Stop();
        }
    }
}
