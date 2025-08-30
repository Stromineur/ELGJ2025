using System;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace NecroMotMicon
{
    public class DescriptionPanel : MonoBehaviour
    {
        [SerializeField] private bool isPreciousWord = false;
        public TextMeshPro titleText;
        public TextMeshPro descriptionText;
        public TextMeshPro powerText;
        public TextMeshPro exhumCost;
        public TextMeshPro writingCost;

        public void OnEnable()
        {
            if (isPreciousWord)
            {
                titleText.text = "Nom du mot";
                descriptionText.text = "Définition du mot";
                powerText.text = "Pouvoirs du mot";
                exhumCost.text = "Coût d'exhumation";
                writingCost.text = "Coût d'écriture";
            }
            else
            {
                titleText.text = "Nom du mot";
                descriptionText.text = "Définition du mot";
                powerText.text = "";
                exhumCost.text = "";
                writingCost.text = "";
            }
        }
    }
}
