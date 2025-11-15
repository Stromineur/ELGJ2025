using FMOD.Studio;
using FMODUnity;
using LTX;
using LTX.Singletons;
using UnityEngine;

namespace Legendhair.Audio
{
    public class AudioManager : MonoSingleton<AudioManager>
    {
        public void PlayOneShot (EventReference sound, Vector3 worldPos)
        {
            RuntimeManager.PlayOneShot(sound, worldPos);
        }

        public void ChangeAmbianceVolume(int volume)
        {
            RuntimeManager.StudioSystem.setParameterByName("Amb_Vol", volume);
        }

        public void ChangeMusicVolume(int volume)
        {
            RuntimeManager.StudioSystem.setParameterByName("Mus_Vol", volume);
        }

        public void ChangeSFXVolume(int volume)
        {
            RuntimeManager.StudioSystem.setParameterByName("SFX_Vol 2", volume);
        }

        public EventInstance CreateInstance(EventReference eventReference)
        {
            EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
            return eventInstance;
        }
    }
}
