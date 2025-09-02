using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LucidFactory.UI.Panels;
using NecroMotMicon.Script.FightingPlan;
using NecroMotMicon.Script.FightingPlan.Wave;
using Script.Core;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace NecroMotMicon.Script.Words
{
    public class WordManager: MonoBehaviour
    {
        #region Variables

        [Header("Words info")]
        [ReadOnly] public List<BookWord> wordsList;
        
        [Header("Drag info")]
        [ReadOnly] public GameObject draggedWord;
        [ReadOnly] public BookWord selectedWord;

        [Header("Unlock words")] 
        [ReadOnly][SerializeField] private int totalInk;
        [ReadOnly][SerializeField] private bool isInterphase;
        [SerializeField] private TextMeshPro inkText;
        [SerializeField] private GameObject buyPanel;
        [SerializeField] private TextMeshProUGUI buyText;
        
        [FormerlySerializedAs("descriptionPanel")]
        [Header("Word description info")]
        [SerializeField] private GameObject descriptionZone; 
        public GameObject preciousWordDescriptionPanel;
        public GameObject badWordDescriptionPanel;
        [SerializeField] private GameObject badWordDataContainer;
        [SerializeField] private GameObject defaultBadWordDescription;
        [ReadOnly][SerializeField] private List<BadWordData> nextBadWords;
        private List<GameObject> displayedBadWords;
        
        private WaveManager _waveManager;
    
        #endregion

        private void Awake()
        {
            totalInk = GameController.GameMetrics.StartInk;
            _waveManager = ServiceLocator.Instance.WaveManager;
            
            wordsList = new List<BookWord>();
            wordsList.Clear();
            
            for (int i = 0; i < transform.childCount; i++)
            {
                wordsList.Add(transform.GetChild(i).GetComponent<BookWord>());
            }
            
            inkText.text = totalInk.ToString();
        }

        private void OnEnable()
        {
            _waveManager.OnWaveEnd += InterphaseStarted;
            _waveManager.OnWaveStarts += InterphaseEnded;
        }

        private void OnDisable()
        {
            _waveManager.OnWaveEnd -= InterphaseStarted;
            _waveManager.OnWaveStarts -= InterphaseEnded;
        }

        // Déclenche les effets du passage à l'interphase (ou sa fin)
        #region Interphase
        private void InterphaseStarted(WaveData _) => InterphaseStarted();

        [Button(ButtonSizes.Large)]
        public void InterphaseStarted()
        {
            isInterphase = true;
            draggedWord.GetComponent<BookWord>().CancelDragAndDrop();
            WordDescriptionEnable();
            GetNextBadWords();
            foreach (BookWord word in wordsList)
            {
                word.canDrag = false;
            }
            Debug.Log("InterphaseStarted");
        }

        private void InterphaseEnded(int obj) => InterphaseEnded();
        
        [Button(ButtonSizes.Large)]
        public void InterphaseEnded()
        {
            isInterphase = false;
            WordDescriptionDisable();
            Debug.Log("InterphaseEnded");
        }
        
        #endregion
        
        // Gère ce qu'il se passe lorsque le joueur clic sur un mot
        #region OnWordClick

        // Se déclenche au moment où l'on clique sur un mot, gères notament la possibilité d'effectuer un dragNdrop
        public void ClickOnWord(GameObject word)
        {
            selectedWord = word.GetComponent<BookWord>();

            if (isInterphase)
            {
                if (!selectedWord.isWritten)
                {
                    TryToBuy();
                }
                else
                {
                    Debug.Log(selectedWord.wordData.wordName + " is already written !");
                }
            }

            if (!isInterphase)
            {
                if (!selectedWord.isWritten)
                {
                    Debug.Log("You can't buy outside of the interphase");
                }
                else if (selectedWord.isWritten && selectedWord.wordData.exhumingCost > totalInk)
                {
                    selectedWord.canDrag = false;
                    Debug.Log("You don't have enough ink");
                }
                else if (selectedWord.isWritten && selectedWord.wordData.exhumingCost <= totalInk)
                {
                    selectedWord.canDrag = true;
                }
            }
        }

        private void TryToBuy()
        {
            if (totalInk < selectedWord.wordData.writingCost)
            {
                Debug.Log("Not enough ink to buy " + selectedWord.wordData.wordName);
            }
            else
            {
                buyPanel.SetActive(true);
                buyText.text = "Écrire " + selectedWord.wordData.wordName + " pour " + selectedWord.wordData.writingCost + " d'encre ?";
            }
        }

        [Button(ButtonSizes.Large)]
        //achète le mot sur lequel le joueur clique (si possible)
        public void BuyWord()
        {
            if (selectedWord != null)
            {
                UpdateTotalInk(selectedWord.wordData.writingCost, false);
                selectedWord.isWritten = true;
                selectedWord.writingPriceText.gameObject.SetActive(false);
                selectedWord.lockImage.SetActive(false);
                Debug.Log(selectedWord.wordData.wordName + " unlocked !");
            }
        }
        
        #endregion
        
        // Est trigger lors de l'achat ou du drop d'un mot
        public void UpdateTotalInk(int inkVariation, bool isInkGain)
        {
            if (isInkGain)
            {
                totalInk += inkVariation;
                inkText.text = totalInk.ToString(); 
            }
            else if (!isInkGain)
            {
                totalInk -= inkVariation;
                inkText.text = totalInk.ToString(); 
            }
        }
        
        #region DescriptionPanels

        private void WordDescriptionEnable()
        {
            descriptionZone.GetComponent<ScaleAnimWorldComponent>().EnableThanScaleUp();
            //preciousWordDescriptionPanel.GetComponent<ScaleAnimWorldComponent>().EnableThanScaleUp();
            //badWordDescriptionPanel.GetComponent<ScaleAnimWorldComponent>().EnableThanScaleUp();
        }
        
        private void WordDescriptionDisable()
        {
            descriptionZone.GetComponent<ScaleAnimWorldComponent>().ScaleDownThenDisable();
            //preciousWordDescriptionPanel.GetComponent<ScaleAnimWorldComponent>().ScaleDownThenDisable();
            //badWordDescriptionPanel.GetComponent<ScaleAnimWorldComponent>().ScaleDownThenDisable();
        }

        [Button(ButtonSizes.Large)]
        private void GetNextBadWords()
        {
            nextBadWords.Clear();
            nextBadWords.AddRange(_waveManager.nextWaveBadWords);
            InstanciateBadWordsPreview();
            //DestroyBadWordsPreview();
        }
        
        private void InstanciateBadWordsPreview()
        {
            foreach (Transform child in badWordDataContainer.transform)
            {
                Destroy(child.gameObject);
            }
            
            foreach (BadWordData badWordData in nextBadWords)
            {
                defaultBadWordDescription.GetComponent<BadWordDescription>().badWordData = badWordData;
                defaultBadWordDescription.GetComponent<SpriteRenderer>().sprite = badWordData.Prefab.GetComponent<SpriteRenderer>().sprite;
                Instantiate(defaultBadWordDescription, badWordDataContainer.transform.position, Quaternion.identity, badWordDataContainer.transform);
            }
        }
        
        #endregion

    }
}
