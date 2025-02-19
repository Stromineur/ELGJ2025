using LTX.ChanneledProperties;
using Spine;
using Spine.Unity;
using UnityEngine;
using UnityEngine.Serialization;

namespace Legendhair.Player.Animation
{
    [CreateAssetMenu(menuName = "ScriptableObjects/PlayerAnimationParameters", fileName = nameof(PlayerAnimationParameters))]
    public class PlayerAnimationParameters : ScriptableObject
    {
        public AnimationReferenceAsset Asset => _asset;
        public PriorityTags Priority => _priority;
        public bool Loop => _loop;
        public float TimeScale => _timeScale;
        public bool UnscaledTime => _unscaledTime;
        public bool Flip => _flip;
        
        [SerializeField] private AnimationReferenceAsset _asset;
        [SerializeField] private PriorityTags _priority;
        [SerializeField] private bool _loop;
        [SerializeField] private float _timeScale = 1;
        [SerializeField] private bool _unscaledTime;
        [SerializeField] private bool _flip;

        public void SetTimeScale(float time)
        {
            _timeScale = _asset.Animation.Duration / time;
            Debug.Log(_timeScale);
        }
    }
}
