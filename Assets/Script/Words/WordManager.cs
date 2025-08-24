using System;
using System.Collections;
using System.Collections.Generic;
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
        public TextMeshPro inkText;
        public GameObject buyPanel;
        public TextMeshProUGUI buyText;
    
        #endregion

        private void Awake()
        {
            wordsList = new List<BookWord>();
            wordsList.Clear();
            for (int i = 0; i < transform.childCount; i++)
            {
                wordsList.Add(transform.GetChild(i).GetComponent<BookWord>());
            }
            
            inkText.text = totalInk.ToString();
        }

        [Button(ButtonSizes.Large)]
        public void InterphaseStarted()
        {
            foreach (BookWord word in wordsList)
            {
                isInterphase = true;
                word.canDrag = false;
            }
            Debug.Log("InterphaseStarted");
        }
        
        [Button(ButtonSizes.Large)]
        public void InterphaseEnded()
        {
            foreach (BookWord word in wordsList)
            {
                isInterphase = false;
                word.canDrag = true;
            }
            Debug.Log("InterphaseEnded");
        }
        
        public void ClickOnWord(GameObject word)
        {
            selectedWord = word.GetComponent<BookWord>();

            if (isInterphase && !selectedWord.isWritten)
            {
                TryToBuy();
            }
            else if (isInterphase && selectedWord.isWritten)
            {
                Debug.Log(selectedWord.wordData.wordName + " is already written !");
            }
            else if (!isInterphase && !selectedWord.isWritten)
            {
                Debug.Log("You can't buy outside of the interphase");
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
                totalInk -= selectedWord.wordData.writingCost;
                selectedWord.isWritten = true;
                selectedWord.writingPriceText.gameObject.SetActive(false);
                selectedWord.lockImage.SetActive(false);
                inkText.text = totalInk.ToString();
                Debug.Log(selectedWord.wordData.wordName + " unlocked !");
            }
        }

    }
}
