using LTX.Singletons;
using Script.FightingPlan.Wave;
using UnityEngine;

namespace Script.Core
{
    public class ServiceLocator : MonoSingleton<ServiceLocator>
    {
        public WaveManager WaveManager
        {
            get
            {
                if (!_waveManager)
                    _waveManager = FindFirstObjectByType<WaveManager>();
                return _waveManager;
            }
        }
        
        public WordManager WordManager
        {
            get
            {
                if (!_wordManager)
                    _wordManager = FindFirstObjectByType<WordManager>();
                return _wordManager;
            }
        }

        private WaveManager _waveManager;
        private WordManager _wordManager;
    }
}
