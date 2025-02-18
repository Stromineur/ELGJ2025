using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script.FightingPlan.Wave
{
    [CreateAssetMenu(fileName = "WaveData", menuName = "Scriptable Objects/WaveData")]
    public class WaveData : ScriptableObject
    {
        public PatternData[] WavePatterns => wavePatterns;
        public int NbPattern => nbPattern;
        public float DelayBetweenMultiplier => delayBetweenMultiplier;
        public float DelayAfterMultiplier => delayAfterMultiplier;

        [SerializeField, LabelText("Pool de briques")] private PatternData[] wavePatterns = { };
        [SerializeField, LabelText("Nombre de briques")] private int nbPattern;
        [SerializeField, LabelText("Multiplicateur de délai")] private float delayBetweenMultiplier = 1;
        [SerializeField, LabelText("Multiplicateur de répit")] private float delayAfterMultiplier = 1;
    }
}
