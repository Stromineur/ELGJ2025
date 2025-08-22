using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace NecroMotMicon.Script.Words
{
    public class WordTemplate_OLD : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        #region Variables
        [ReadOnly] public event Action OnWordDrag;
        [ReadOnly] public event Action OnWordDrop;
    
        private WordManager_OLD _wordManagerOld;
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
            _wordManagerOld = GetComponentInParent<WordManager_OLD>();
            spawnPoint = _wordManagerOld.dragableSpawnPoint;
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
        
            _wordManagerOld.ClickOnWord(gameObject);
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
            _wordManagerOld.draggedWord = gameObject;
            OnWordDrag?.Invoke();
        }
    
        private void StopDragAndDrop()
        {
            isDragging = false;
            wordDraggableObject.SetActive(false);
            _wordManagerOld.draggedWord = null;
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
}
