using NecroMotMicon.Script.Words;
using Script.Core;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace NecroMotMicon.Script.FightingPlan.Feedback
{
    public class GraveLightUp : MonoBehaviour
    {
        [SerializeField] private FightingLane _fightingLane;
        private Light2D _light2D;
        //private DragNDropEvents_OLD _dragNDropEventsOld;
        private DragNDropEvents _dragNDropEvents;

        private void Awake()
        {
            _light2D = GetComponentInChildren<Light2D>();
            _light2D.gameObject.SetActive(true);
            _light2D.enabled = false;
            _dragNDropEvents = ServiceLocator.Instance.DragNDropEvents;
            //_dragWord = ServiceLocator.Instance.DragNDropEventsOld;
        }

        private void OnEnable()
        {
            //_dragNDropEventsOld.OnWordDrag += OnDragStart;
            _dragNDropEvents.OnWordStartDrag += OnDragStart;
            //_dragNDropEventsOld.OnWordDrop += OnDragStop;
            _dragNDropEvents.OnWordDrop += OnDragStop;
            _fightingLane.OnCanSpawn += OnCanSpawn;
            _fightingLane.OnCantSpawn += OnCantSpawn;
        }

        private void OnDisable()
        {
            //_dragNDropEventsOld.OnWordDrag -= OnDragStart;
            _dragNDropEvents.OnWordStartDrag -= OnDragStart;
            //_dragNDropEventsOld.OnWordDrop -= OnDragStop;
            _dragNDropEvents.OnWordDrop -= OnDragStop;
            _fightingLane.OnCanSpawn -= OnCanSpawn;
            _fightingLane.OnCantSpawn -= OnCantSpawn;
        }

        private void OnCanSpawn()
        {
            _light2D.gameObject.SetActive(true);
        }

        private void OnCantSpawn()
        {
            _light2D.gameObject.SetActive(false);
        }

        private void OnDragStart()
        {
            _light2D.enabled = true;
        }

        private void OnDragStop()
        {
            _light2D.enabled = false;
        }
    }
}
