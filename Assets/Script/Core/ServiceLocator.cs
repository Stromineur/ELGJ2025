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
        
        public WordManager_OLD WordManagerOld
        {
            get
            {
                if (!_wordManagerOld)
                    _wordManagerOld = FindFirstObjectByType<WordManager_OLD>();
                return _wordManagerOld;
            }
        }
        
        public DragNDropEvents_OLD DragNDropEventsOld
        {
            get
            {
                if (!_dragNDropEventsOld)
                    _dragNDropEventsOld = FindFirstObjectByType<DragNDropEvents_OLD>();
                return _dragNDropEventsOld;
            }
        }

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
        private WordManager_OLD _wordManagerOld;
        private DragNDropEvents_OLD _dragNDropEventsOld;
        private DragNDropEvents _dragNDropEvents;
    }
}
