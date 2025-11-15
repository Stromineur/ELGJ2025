using FMODUnity;
using UnityEngine;
using FMOD;
using FMOD.Studio;
using Legendhair.Audio;

namespace NecroMotMicon.Script.FightingPlan.Sound
{
    public class WordSoundController : MonoBehaviour
    {
        [SerializeField] private EventReference AttackEvent;
        [SerializeField] private EventReference SpawnEvent;

        internal FightingWord word;

        private void Awake()
        {
            word = GetComponent<FightingWord>();
            PlayOneShot(SpawnEvent);
        }

        private void OnEnable()
        {
            word.OnAttack += PlayAttackSound;
        }

        private void OnDisable()
        {
            word.OnAttack -= PlayAttackSound;
        }

        private void PlayAttackSound()
        {
            PlayOneShot(AttackEvent);
        }
        
        public void PlayOneShot(EventReference eventReference)
        {
            PlayOneShot(eventReference, transform.position);
        }

        public void PlayOneShot(EventReference eventReference, Vector3 position)
        {
            RuntimeManager.PlayOneShot(eventReference, position);
        }

        public EventInstance CreateInstance(EventReference soundInstance)
        {
            EventInstance eventInstance = AudioManager.Instance.CreateInstance(soundInstance);
            RuntimeManager.AttachInstanceToGameObject(eventInstance, gameObject);
            return eventInstance;
        }
    }
}
