using Script.Words;
using UnityEngine;

namespace Script.FightingPlan
{
    [CreateAssetMenu(fileName = "BadWordData", menuName = "Scriptable Objects/BadWordData")]
    public class BadWordData : ScriptableObject, IFightingData
    {
        public FightingWord Prefab => prefab;
        public float Speed => speed;
        public float Hp => hp;

        [SerializeField] private FightingWord prefab;
        [SerializeField] private float speed;
        [SerializeField] private float hp;
    }
}
