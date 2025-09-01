using System;
using NecroMotMicon.Script.FightingPlan;
using NecroMotMicon.Script.Words;
using Script.Core;
using UnityEngine;

namespace NecroMotMicon
{
    public class BadWordDescription : MonoBehaviour
    {
        public BadWordData badWordData;
        private DescriptionPanel _descriptionPanel;
        private WordManager _wordManager;

        private void Awake()
        {
            _wordManager = ServiceLocator.Instance.WordManager;
            _descriptionPanel = _wordManager.badWordDescriptionPanel.GetComponent<DescriptionPanel>();
        }
        
        private void OnMouseEnter()
        {
            _descriptionPanel.titleText.text = badWordData.WordName;
            _descriptionPanel.descriptionText.text = "Définition : " + badWordData.WordDescription;
        }
    }
}
