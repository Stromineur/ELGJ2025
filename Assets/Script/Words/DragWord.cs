using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace NecroMotMicon.Script.Words
{
    public class DragWord : MonoBehaviour
    {
        #region Variables
        
        [ReadOnly] public event Action OnWordStartDrag;
        [ReadOnly] public event Action OnWordDrop;
    
        private WordManager _wordManager;
        public WordData wordData;
        [ReadOnly] public TextMeshPro wordText;
    
        [ReadOnly] public GameObject lockImage;
        public bool isWritten;
    
        private bool isInScene;
        private bool isDragging;
        private Vector3 mousePosition;
        private GameObject wordDraggableObject;
        private Transform spawnPoint;

        #endregion
        
        private void Awake()
        {
            _wordManager = GetComponentInParent<WordManager>();
            spawnPoint = _wordManager.draggableSpawnPoint;
            lockImage = transform.GetChild(0).gameObject;
            wordText = GetComponentInChildren<TextMeshPro>();
        }
        
        private void Start()
        {
            wordText.text = wordData.wordName;
        }
        private void OnMouseDown()
        {
            if (isWritten && !isInScene)
            {
                InstanciateWord();
            }

            if (isWritten && isInScene)
            {
                StartDragAndDrop();
            }
        
            _wordManager.ClickOnWord(gameObject);
        }

        private void OnMouseDrag()
        {
            if (isDragging && wordDraggableObject != null)
            {
                mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePosition.z = 0;
                wordDraggableObject.transform.position = mousePosition;
            }
        }

        private void OnMouseUp()
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
            _wordManager.draggedWord = gameObject;
            OnWordStartDrag?.Invoke();
        }
        
        private void StopDragAndDrop()
        {
            isDragging = false;
            wordDraggableObject.SetActive(false);
            wordDraggableObject.transform.position = transform.position;
            _wordManager.draggedWord = null;
            OnWordDrop?.Invoke();
        }
        
        private void InstanciateWord() // instancie le préfab variant (ne contenant ni ce script ni collider, pour éviter les conflits de OnMouseDrop) à l'emplacement du mot
        {
            wordDraggableObject = Instantiate(wordData.wordPrefab, Vector3.zero, Quaternion.identity, spawnPoint);
            wordDraggableObject.GetComponent<Transform>().position = GetComponent<Transform>().position;
            wordDraggableObject.GetComponentInChildren<TextMeshPro>().text = wordData.wordName;
            wordDraggableObject.SetActive(false);
            isInScene = true;
        }
    }
}
