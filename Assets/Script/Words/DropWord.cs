using System;
using NecroMotMicon.Script.FightingPlan;
using Script.Core;
using Unity.Collections;
using UnityEngine;

namespace NecroMotMicon.Script.Words
{
    public class DropWord : MonoBehaviour
    {
        #region Variables

        [SerializeField, ReadOnly] private GameObject hoverElement;
        [ReadOnly] public GameObject droppedElement;
        [ReadOnly] public bool isOccupied;
        
        [Header("Zone type")]
        public bool isFightingLane = false;
        public bool isWord = false;
        
        private WordManager _wordManager;
        private FightingLane _fightingLane;
        private FightingWord _fightingWord;
    
        #endregion
        
        private void Awake()
        {
            _wordManager = ServiceLocator.Instance.WordManager;
            if (isFightingLane)
            {
                _fightingLane = GetComponent<FightingLane>();
            }
        }

        private void OnEnable()
        {
            if (isWord)
            {
                _fightingWord = GetComponent<FightingWord>();
                _fightingWord.OnSpawn += SetFightLane;
            }
        }
        
        private void OnDisable()
        {
            if (isWord)
            {
                _fightingWord.OnSpawn -= SetFightLane;
            }
        }

        private void SetFightLane()
        {
            _fightingLane = _fightingWord.FightingLane;
        }

        public void OnMouseEnter()
        {
            if (_wordManager.draggedWord != null)
            {
                hoverElement = _wordManager.draggedWord;
                hoverElement.GetComponent<BookWord>().OnWordDrop += OnDrop;
            }
        }

        public void OnMouseExit()
        {
            if (_wordManager.draggedWord != null)
            {
                hoverElement.GetComponent<BookWord>().OnWordDrop -= OnDrop;
                hoverElement = null;
            }
        }

        private void OnDrop()
        {
            if (hoverElement != null && _fightingLane.CanSpawnPrecious)
            {
                droppedElement = hoverElement;
                isOccupied = true;
                _fightingLane.Spawn(droppedElement.GetComponent<BookWord>().wordData, null);
                hoverElement.GetComponent<BookWord>().OnWordDrop -= OnDrop;
                InkLoss(hoverElement.GetComponent<BookWord>());
                hoverElement = null;
            }
        }

        private void InkLoss(BookWord bookWord)
        {
            bookWord._wordManager.UpdateTotalInk(bookWord.wordData.exhumingCost, false);
        }
    }
}
