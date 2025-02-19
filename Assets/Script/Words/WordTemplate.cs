using System;
using Script.Words;
using Sirenix.OdinInspector;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class WordTemplate : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    #region Variables
    [ReadOnly] public event Action OnWordDrag;
    [ReadOnly] public event Action OnWordDrop;
    
    private WordManager wordManager;
    [ReadOnly] public WordData wordData;
    [ReadOnly] public TextMeshProUGUI wordText;
    
    [ReadOnly] public GameObject lockImage;
    [ReadOnly] public bool isWritten;
    
    private bool isInScene;
    private bool isDragging;
    private GameObject wordDraggableObject;
    private Transform spawnPoint;
        
    #endregion

    private void Awake()
    {
        wordManager = GetComponentInParent<WordManager>();
        spawnPoint = wordManager.dragableSpawnPoint;
        lockImage = transform.GetChild(0).gameObject;
        wordText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        wordText.text = wordData.wordName;
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
        if (isWritten && !isInScene)
        {
            InstanciateWord();
        }

        if (isWritten && isInScene)
        {
            StartDragAndDrop();
        }
        
        wordManager.ClickOnWord(gameObject);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (wordDraggableObject != null)
        {
            StopDragAndDrop();
        }
    }

    private void StartDragAndDrop()
    {
        isDragging = true;
        wordDraggableObject.SetActive(true);
        wordManager.draggedWord = gameObject;
        OnWordDrag?.Invoke();
    }
    
    private void StopDragAndDrop()
    {
        isDragging = false;
        wordDraggableObject.SetActive(false);
        wordManager.draggedWord = null;
        OnWordDrop?.Invoke();
    }

    private void InstanciateWord() // instancie le préfab variant (ne contenant pas ce script) à l'emplacement du mot
    {
        wordDraggableObject = Instantiate(wordData.wordPrefab, Vector3.zero, Quaternion.identity, spawnPoint);
        wordDraggableObject.GetComponent<RectTransform>().position = GetComponent<RectTransform>().position;
        //wordDraggableObject.GetComponentInChildren<Image>().sprite = wordData.wordSprite;
        wordDraggableObject.GetComponentInChildren<TextMeshProUGUI>().text = wordData.wordName;
        wordDraggableObject.SetActive(false);
        isInScene = true;
    }
}
