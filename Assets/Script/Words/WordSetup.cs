using System;
using Script.Words;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class WordSetup : MonoBehaviour
{
    private WordData wordData;
    private Sprite wordSprite;
    
    private void Start()
    {
        wordData = GetComponent<WordTemplate>().wordData;
        wordSprite = wordData.wordSprite;
        GetComponentInChildren<Image>().sprite = wordSprite;
    }
}
