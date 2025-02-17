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

        [SerializeField] private PatternData[] wavePatterns = { };
        [SerializeField] private int nbPattern;
    }
}
