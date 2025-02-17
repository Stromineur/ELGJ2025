using System;
using Script.Words;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WordTemplate : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    #region Variables

    [SerializeField] private WordData wordData;
    [SerializeField] private bool isDragable;
    [SerializeField] private bool isDevelopped;
    [SerializeField] private bool isInScene;
    [SerializeField] private bool isDragging;

    [SerializeField] private GameObject wordDraggableObject;
    
    #endregion

    private void Start()
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = wordData.wordName;
    }

    private void Update()
    {
        if (isDragging && wordDraggableObject != null)
        {
            wordDraggableObject.transform.position = Input.mousePosition;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log(wordData.wordName);
        Debug.Log(wordData.wordDescription);
        
        if (isDragable && !isInScene)
        {
            InstanciateWord();
        }

        if (isDragable && isInScene)
        {
            StartDragAndDrop();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
        wordDraggableObject.SetActive(false);
    }

    private void StartDragAndDrop()
    {
        wordDraggableObject.SetActive(true);
        isDragging = true;
    }

    private void InstanciateWord() // instancie le préfab variant (ne contenant pas ce script) à l'emplacement du mot
    {
        wordDraggableObject = Instantiate(wordData.wordPrefab, Vector3.zero, Quaternion.identity, gameObject.transform);
        wordDraggableObject.SetActive(false);
        wordDraggableObject.GetComponent<RectTransform>().position = gameObject.GetComponent<RectTransform>().position;
        wordDraggableObject.GetComponent<TextMeshProUGUI>().text = wordData.wordName;
        isInScene = true;
    }
}
