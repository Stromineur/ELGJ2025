using UnityEngine;

namespace Script.Words
{
    [CreateAssetMenu(fileName = "WordData", menuName = "Scriptable Objects/WordData")]
    public partial class WordData : ScriptableObject
    {
        private float ExhumingTime => exhumingTime;
        private float Speed => speed;
        private float BaseDamage => baseDamage;
        private EffectType EffectType => effectType;
        private string[] StrongAgainst => strongAgainst;
        private float BoostedDamage => boostedDamage;
        
        [SerializeField, InspectorName("Temps d'exhumation")] private float exhumingTime;
        [SerializeField, InspectorName("Vitesse")] private float speed;
        [SerializeField, InspectorName("Dégâts")] private float baseDamage;
        [SerializeField, InspectorName("Effet")] private EffectType effectType;
        [SerializeField, InspectorName("Prévalence")] private string[] strongAgainst;
        [SerializeField, InspectorName("Dégâts de prévalence")] private float boostedDamage;
    }

    public enum EffectType
    {
        
    }
}
