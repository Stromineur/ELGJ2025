using Sirenix.OdinInspector;
using UnityEngine;

namespace Script.FightingPlan.Wave
{
    [CreateAssetMenu(fileName = "PatternData", menuName = "Scriptable Objects/PatternData")]
    public class PatternData : ScriptableObject
    {
        public BadWordData BadWord => badWord;
        public int MinNumber => minNumber;
        public int MaxNumber => maxNumber;
        public float DelayBetween => delayBetween;
        public float DelayAfter => delayAfter;
        
        [SerializeField, LabelText("Type")] private BadWordData badWord;
        [SerializeField, LabelText("MinNombre")] private int minNumber;
        [SerializeField, LabelText("MaxNombre")] private int maxNumber;
        [SerializeField, LabelText("Délai")] private float delayBetween;
        [SerializeField, LabelText("Répit")] private float delayAfter;
    }
}
