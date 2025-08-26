using NecroMotMicon.Script.FightingPlan;
using Unity.Collections;
using UnityEngine;

namespace NecroMotMicon.Script.Words
{
    public class DropWord : MonoBehaviour
    {
        #region Variables

        [SerializeField, ReadOnly] private GameObject hoverElement;
        [SerializeField, ReadOnly] public GameObject droppedElement;
        [SerializeField, ReadOnly] public bool isOccupied;
    
        private WordManager _wordManager;
        private FightingLane fightingLane;
    
        #endregion
        
        private void Awake()
        {
            _wordManager = GameObject.Find("WordZone").GetComponent<WordManager>();
            fightingLane = GetComponent<FightingLane>();
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
            if (hoverElement != null && fightingLane.CanSpawnPrecious)
            {
                droppedElement = hoverElement;
                isOccupied = true;
                fightingLane.Spawn(droppedElement.GetComponent<BookWord>().wordData, null);
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
