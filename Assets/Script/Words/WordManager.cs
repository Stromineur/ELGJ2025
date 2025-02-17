using System;
using System.Collections.Generic;
using UnityEngine;

public class WordManager : MonoBehaviour
{
    [SerializeField] private List<ScriptableObject> words;
    [SerializeField] private List<GameObject> wordsGameObjects;

    private void Awake()
    {
        foreach (GameObject words in wordsGameObjects)
        {
            
        }
    }
}
