using LTX.Singletons;
using NecroMotMicon.Script.FightingPlan.Wave;
using NecroMotMicon.Script.Words;
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
        
        /*
        public DragNDropEvents_OLD DragNDropEventsOld
        {
            get
            {
                if (!_dragNDropEventsOld)
                    _dragNDropEventsOld = FindFirstObjectByType<DragNDropEvents_OLD>();
                return _dragNDropEventsOld;
            }
        }
        */

        public DragNDropEvents DragNDropEvents
        {
            get
            {
                if (!_dragNDropEvents)
                    _dragNDropEvents = FindFirstObjectByType<DragNDropEvents>();
                return _dragNDropEvents;
            }
        }
        
        private WaveManager _waveManager;
        private WordManager _wordManager;
        //private DragNDropEvents_OLD _dragNDropEventsOld;
        private DragNDropEvents _dragNDropEvents;
    }
}
