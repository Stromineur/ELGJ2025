using LTX.ChanneledProperties.Priorities;
using UnityEngine;

namespace NecroMotMicon.Script.Animation
{
    [CreateAssetMenu(menuName = "ScriptableObjects/ZombieAnimationParameters", fileName = nameof(WordAnimationParameters))]
    public class WordAnimationParameters : ScriptableObject
    {
        public string AssetName => _assetName;
        public PriorityTags Priority => _priority;
        public bool Loop => _loop;
        public float TimeScale => _timeScale;
        public bool UnscaledTime => _unscaledTime;
        public bool Flip => _flip;
        
        [SerializeField] private string _assetName;
        [SerializeField] private PriorityTags _priority;
        [SerializeField] private bool _loop;
        [SerializeField] private float _timeScale = 1;
        [SerializeField] private bool _unscaledTime;
        [SerializeField] private bool _flip;
    }
}
