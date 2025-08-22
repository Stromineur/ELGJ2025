using NecroMotMicon.Script.FightingPlan;
using Unity.Collections;
using UnityEngine;

namespace NecroMotMicon.Script.Words
{
    public class DropWord_OLD : MonoBehaviour
    {
        #region Variables

        [SerializeField, ReadOnly] private GameObject hoverElement;
        [ReadOnly] public GameObject droppedElement;
        [ReadOnly] public bool isOccupied;
    
        private WordManager_OLD _wordManagerOld;
        private FightingLane fightingLane;
    
        #endregion
    
        private void Awake()
        {
            _wordManagerOld = GameObject.Find("BookPanel").GetComponent<WordManager_OLD>();
            fightingLane = GetComponent<FightingLane>();
        }

        public void OnMouseEnter()
        {
            if (_wordManagerOld.draggedWord != null)
            {
                hoverElement = _wordManagerOld.draggedWord;
                hoverElement.GetComponent<WordTemplate_OLD>().OnWordDrop += OnDrop;
            }
        }

        public void OnMouseExit()
        {
            if (_wordManagerOld.draggedWord != null)
            {
                hoverElement.GetComponent<WordTemplate_OLD>().OnWordDrop -= OnDrop;
                hoverElement = null;
            }
        }

        private void OnDrop()
        {
            if (hoverElement != null && fightingLane.CanSpawnPrecious)
            {
                droppedElement = hoverElement;
                isOccupied = true;
                fightingLane.Spawn(droppedElement.GetComponent<WordTemplate_OLD>().wordData, null);
                hoverElement.GetComponent<WordTemplate_OLD>().OnWordDrop -= OnDrop;
                hoverElement = null;
            }
        }
    }
}
