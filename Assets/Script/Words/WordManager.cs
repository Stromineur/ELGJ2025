using System;
using System.Collections.Generic;
using System.Linq;
using Script.Words;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class WordManager : MonoBehaviour
{
    #region Variables
    
    [Header("Book construction")]
    [SerializeField] private GameObject wordsLocation;
    public Transform dragableSpawnPoint;
    [SerializeField] private List<WordData> wordsData;
    [SerializeField, ReadOnly] private List<GameObject> wordsGameObjects;

    [Header("Word dragging")]
    [SerializeField, ReadOnly] private GameObject selectedWord;
    [ReadOnly] public GameObject draggedWord;
    
    [Header("Word writing")]
    [SerializeField, ReadOnly] private GameObject writingWord;
    
    [Header("Word description")]
    [SerializeField] private GameObject wordNamePanel;
    [SerializeField] private GameObject wordDescriptionPanel;
    [SerializeField] private GameObject wordEffectPanel;
    
    #endregion
    
    private void Awake()
    {
        for (int i = 0; i < wordsLocation.transform.childCount; i++)
        {
            wordsGameObjects.Add(wordsLocation.transform.GetChild(i).gameObject);
        }
        
        for (int i = 0; i < wordsGameObjects.Count; i++)
        {
            wordsGameObjects[i].GetComponent<WordTemplate>().wordData = wordsData[i%wordsData.Count];
        }
    }

    public void ClickOnWord(GameObject word)
    {
        selectedWord = word;
        wordNamePanel.GetComponent<TextMeshProUGUI>().text = selectedWord.GetComponent<WordTemplate>().wordData.wordName;
        wordDescriptionPanel.GetComponent<TextMeshProUGUI>().text = selectedWord.GetComponent<WordTemplate>().wordData.wordDescription;
        wordEffectPanel.GetComponent<TextMeshProUGUI>().text = selectedWord.GetComponent<WordTemplate>().wordData.wordEffect;
    }

    #region Word writing

    public void WriteWord()
    {
        if (!selectedWord.GetComponent<WordTemplate>().isWritten)
        {
            writingWord = selectedWord;
            Invoke("WritingFinished", writingWord.GetComponent<WordTemplate>().wordData.writingTime);
        }
    }

    private void WritingFinished()
    {
        writingWord.GetComponent<WordTemplate>().isWritten = true;
        writingWord.GetComponent<WordTemplate>().lockImage.SetActive(false);
    }

    #endregion
    

    #region Word ordering

    [Button(ButtonSizes.Large)]
    public void OrderWordsByWritten()
    {
        foreach (GameObject word in wordsGameObjects)
        {
            if (!word.GetComponent<WordTemplate>().isWritten)
            {
                word.gameObject.SetActive(false);
            }
            else
            {
                word.gameObject.SetActive(true);
            }
        }
    }
    
    [Button(ButtonSizes.Large)]
    public void OrderWordsByNonWritten()
    {
        foreach (GameObject word in wordsGameObjects)
        {
            if (word.GetComponent<WordTemplate>().isWritten)
            {
                word.gameObject.SetActive(false);
            }
            else
            {
                word.gameObject.SetActive(true);
            }
        }
    }
    
    [Button(ButtonSizes.Large)]
    public void RemoveOrder()
    {
        foreach (GameObject word in wordsGameObjects)
        {
            word.gameObject.SetActive(true);
        }
    }

    #endregion
}
