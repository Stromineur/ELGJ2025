using System;
using Script.Words;
using Unity.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropWord : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    #region Variables

    [SerializeField, ReadOnly] private GameObject hoverElement;
    [ReadOnly] public GameObject droppedElement;
    [ReadOnly] public bool isOccupied;
    
    private WordManager wordManager;
    
    #endregion
    
    private void Awake()
    {
        wordManager = GameObject.Find("BookPanel").GetComponent<WordManager>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (wordManager.draggedWord != null)
        {
            Debug.Log(wordManager.draggedWord.name);
            hoverElement = wordManager.draggedWord;
            hoverElement.GetComponent<WordTemplate>().OnWordDrop += OnDrop;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (wordManager.draggedWord != null)
        {
            Debug.Log(wordManager.draggedWord.name);
            hoverElement.GetComponent<WordTemplate>().OnWordDrop -= OnDrop;
            hoverElement = null;
        }
    }

    private void OnDrop()
    {
        if (hoverElement != null && !isOccupied)
        {
            droppedElement = hoverElement;
            isOccupied = true;
            hoverElement.GetComponent<WordTemplate>().OnWordDrop -= OnDrop;
            hoverElement = null;
        }
    }
}
