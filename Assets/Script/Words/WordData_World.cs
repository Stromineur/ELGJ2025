using Script.FightingPlan;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Script.Words
{
    [CreateAssetMenu(fileName = "WordData", menuName = "Scriptable Objects/WordData")]
    public partial class WordData : IFightingData
    {
        public float ExhumingTime => exhumingTime;
        public float Speed => speed;
        public float BaseDamage => baseDamage;
        public EffectType EffectType => effectType;
        public string[] StrongAgainst => strongAgainst;
        public float BoostedDamage => boostedDamage;
        public FightingWord Prefab => prefab;
        
        [SerializeField, FoldoutGroup("InGame"), LabelText("Temps d'exhumation")] private float exhumingTime;
        [SerializeField, FoldoutGroup("InGame"), LabelText("Vitesse")] private float speed;
        [SerializeField, FoldoutGroup("InGame"), LabelText("Dégâts")] private float baseDamage;
        [SerializeField, FoldoutGroup("InGame"), LabelText("Effet")] private EffectType effectType;
        [SerializeField, FoldoutGroup("InGame"), LabelText("Prévalence")] private string[] strongAgainst;
        [SerializeField, FoldoutGroup("InGame"), LabelText("Dégâts de prévalence")] private float boostedDamage;
        [SerializeField, FoldoutGroup("InGame"), LabelText("Prefab")] private FightingWord prefab;
    }

    public enum EffectType
    {
        
    }
}
