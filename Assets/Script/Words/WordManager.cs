using System;
using System.Collections;
using System.Collections.Generic;
using LucidFactory.UI.Panels;
using NecroMotMicon.Script.FightingPlan.Wave;
using Script.Core;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NecroMotMicon.Script.Words
{
    public class WordManager: MonoBehaviour
    {
        #region Variables

        [Header("Words info")]
        public List<BookWord> wordsList;
        
        [Header("Drag info")]
        [ReadOnly] public GameObject draggedWord;
        [ReadOnly] public BookWord selectedWord;

        [Header("Unlock words")] 
        public int totalInk;
        public bool isInterphase;
        
        [Header("Object references")]
        public GameObject preciousWordDescriptionGO;
        public GameObject badWordDescriptionGO;
        public TextMeshPro inkText;
        public GameObject buyPanel;
        public TextMeshProUGUI buyText;
    
        #endregion

        private void Awake()
        {
            totalInk = GameController.GameMetrics.StartInk;
            
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
            ServiceLocator.Instance.WaveManager.OnWaveEnd += InterphaseStarted;
            ServiceLocator.Instance.WaveManager.OnWaveStarts += InterphaseEnded;
        }

        private void OnDisable()
        {
            ServiceLocator.Instance.WaveManager.OnWaveEnd -= InterphaseStarted;
            ServiceLocator.Instance.WaveManager.OnWaveStarts -= InterphaseEnded;
        }

        private void InterphaseStarted(WaveData _) => InterphaseStarted();

        [Button(ButtonSizes.Large)]
        public void InterphaseStarted()
        {
            isInterphase = true;
            preciousWordDescriptionGO.SetActive(true);
            badWordDescriptionGO.SetActive(true);
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
            preciousWordDescriptionGO.SetActive(false);
            badWordDescriptionGO.SetActive(false);
            Debug.Log("InterphaseEnded");
        }
        
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

        //Est trigger lors de l'achat ou du drop d'un mot
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

    }
}
