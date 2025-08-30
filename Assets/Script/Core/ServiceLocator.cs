using LTX.Singletons;
using LucidFactory.UI.Panels;
using NecroMotMicon.Script.FightingPlan;
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
        
        public PlayerArea PlayerArea
        {
            get
            {
                if (!_playerArea)
                    _playerArea = FindFirstObjectByType<PlayerArea>();
                return _playerArea;
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
        
        public LF_TabManager LF_TabManager
        {
            get
            {
                if (!_lfTabManager)
                    _lfTabManager = FindFirstObjectByType<LF_TabManager>();
                return _lfTabManager;
            }
        }
        
        private WaveManager _waveManager;
        private WordManager _wordManager;
        private PlayerArea _playerArea;
        //private DragNDropEvents_OLD _dragNDropEventsOld;
        private DragNDropEvents _dragNDropEvents;
        private LF_TabManager _lfTabManager;
    }
}
