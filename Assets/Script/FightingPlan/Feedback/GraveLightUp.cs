using System;
using Script.Core;
using Script.Words;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Script.FightingPlan.Feedback
{
    public class GraveLightUp : MonoBehaviour
    {
        [SerializeField] private FightingLane _fightingLane;
        private Light2D _light2D;
        private DragNDropEvents dragNDropEvents;

        private void Awake()
        {
            _light2D = GetComponentInChildren<Light2D>();
            _light2D.gameObject.SetActive(true);
            _light2D.enabled = false;
            dragNDropEvents = ServiceLocator.Instance.DragNDropEvents;
        }

        private void OnEnable()
        {
            dragNDropEvents.OnWordDrag += OnDragStart;
            dragNDropEvents.OnWordDrop += OnDragStop;
            _fightingLane.OnCanSpawn += OnCanSpawn;
            _fightingLane.OnCantSpawn += OnCantSpawn;
        }

        private void OnDisable()
        {
            dragNDropEvents.OnWordDrag -= OnDragStart;
            dragNDropEvents.OnWordDrop -= OnDragStop;
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
