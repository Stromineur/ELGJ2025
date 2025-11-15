using UnityEngine;

namespace NecroMotMicon.Script.FightingPlan.Sound
{
    [RequireComponent(typeof(WordSoundController))]
    public abstract class SpecificWordSoundController : MonoBehaviour
    {
        protected WordSoundController soundController;

        protected virtual void Awake()
        {
            soundController = GetComponent<WordSoundController>();
        }

        private void OnEnable()
        {
            InternalOnEnable();
        }

        protected abstract void InternalOnEnable();

        private void OnDisable()
        {
            InternalOnDisable();
        }

        protected abstract void InternalOnDisable();
    }
}
