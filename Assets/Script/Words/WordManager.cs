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
        
        [ReadOnly] public GameObject draggedWord;
        public Transform draggableSpawnPoint;
        public GameObject selectedWord;
    
        #endregion
        
        public void ClickOnWord(GameObject word)
        {
            selectedWord = word;
        }

    }
}
