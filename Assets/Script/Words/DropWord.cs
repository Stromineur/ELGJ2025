using System;
using Script.FightingPlan;
using Script.Words;
using Unity.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropWord : MonoBehaviour
{
    #region Variables

    [SerializeField, ReadOnly] private GameObject hoverElement;
    [ReadOnly] public GameObject droppedElement;
    [ReadOnly] public bool isOccupied;
    
    private WordManager wordManager;
    private FightingLane fightingLane;
    
    #endregion
    
    private void Awake()
    {
        wordManager = GameObject.Find("BookPanel").GetComponent<WordManager>();
        fightingLane = GetComponent<FightingLane>();
    }

    public void OnMouseEnter()
    {
        if (wordManager.draggedWord != null)
        {
            hoverElement = wordManager.draggedWord;
            hoverElement.GetComponent<WordTemplate>().OnWordDrop += OnDrop;
        }
    }

    public void OnMouseExit()
    {
        if (wordManager.draggedWord != null)
        {
            hoverElement.GetComponent<WordTemplate>().OnWordDrop -= OnDrop;
            hoverElement = null;
        }
    }

    private void OnDrop()
    {
        if (hoverElement != null && fightingLane.CanSpawnPrecious)
        {
            droppedElement = hoverElement;
            isOccupied = true;
            fightingLane.Spawn(droppedElement.GetComponent<WordTemplate>().wordData, null);
            hoverElement.GetComponent<WordTemplate>().OnWordDrop -= OnDrop;
            hoverElement = null;
        }
    }
}
