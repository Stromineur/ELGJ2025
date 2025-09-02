using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace NecroMotMicon.Script.Words
{
    public class BookWord : MonoBehaviour
    {
        #region Variables
        
        [ReadOnly] public event Action OnWordStartDrag;
        [ReadOnly] public event Action OnWordDrop;
    
        [Header("Object references")]
        [HideInInspector] public WordManager _wordManager;
        private DescriptionPanel _descriptionPanel;
        public WordData wordData;
        public TextMeshPro wordText;
        public GameObject lockImage;
        public TextMeshPro writingPriceText; 
        public TextMeshPro exhumingPriceText; 
        
        public bool isWritten;
        public bool canDrag;
    
        private bool isInScene;
        private bool isDragging;
        private Vector3 mousePosition;
        private GameObject wordDraggableObject;

        #endregion
        
        private void Awake()
        {
            _wordManager = GetComponentInParent<WordManager>();
            _descriptionPanel = _wordManager.preciousWordDescriptionPanel.GetComponent<DescriptionPanel>();
        }
        
        private void Start()
        {
            wordText.text = wordData.wordName;
            writingPriceText.text = wordData.writingCost.ToString();
            exhumingPriceText.text = wordData.exhumingCost.ToString();
            
        }
        
        private void OnMouseEnter()
        {
            _descriptionPanel.titleText.text = wordData.wordName;
            _descriptionPanel.descriptionText.text = "Définition : " + wordData.wordDescription;
            _descriptionPanel.powerText.text = "Effets : " + wordData.wordEffect;
            _descriptionPanel.exhumCost.text = "Coût d'exhumation : " + wordData.exhumingCost.ToString();
            _descriptionPanel.writingCost.text = "Coût d'écriture : " + wordData.writingCost.ToString();
        }
        
        private void OnMouseDown()
        {
            _wordManager.ClickOnWord(gameObject);
            
            if (canDrag && isWritten && !isInScene)
            {
                InstanciateWord();
            }

            if (canDrag && isWritten && isInScene)
            {
                StartDragAndDrop();
            }
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
            if (canDrag && wordDraggableObject != null)
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
            wordDraggableObject = Instantiate(wordData.wordPrefab, Vector3.zero, Quaternion.identity);
            wordDraggableObject.GetComponent<Transform>().position = GetComponent<Transform>().position;
            wordDraggableObject.GetComponentInChildren<TextMeshPro>().text = wordData.wordName;
            wordDraggableObject.SetActive(false);
            isInScene = true;
        }

        public void CancelDragAndDrop()
        {
            isDragging = false;
            wordDraggableObject.SetActive(false);
            wordDraggableObject.transform.position = transform.position;
            _wordManager.draggedWord = null;
        }
    }
}
