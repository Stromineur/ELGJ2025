using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace Untold.Core.Stories.UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class HoverText : MonoBehaviour
    {
        [SerializeField] private HoverDictionaryData hoverDictionary;
        public TextMeshProUGUI _textMeshProUGUI;
        private string _tempText;
        private bool _isActive = false;

        public bool IsActive
        {
            get => _isActive;
            private set => _isActive = value;
        }

        private int _tempLinkTaggedText;

        public void Awake()
        {
            _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        }

        private void Start()
        {
            CreateLinks();
        }

        private void OnEnable()
        {
            HoverManager.Instance.AddHover(this);
        }

        private void OnDisable()
        {
            HoverManager.Instance.RemoveHover(this);
        }

        /// <summary>
        /// Create Links over the words contains in dictionary and make them bold
        /// </summary>
        private void CreateLinks()
        {
            foreach (HoverInfo _word in hoverDictionary.WordsToHover)
            {
                if(_textMeshProUGUI.text.Contains(_word.Title)) Debug.Log("word " + _word.Title + " found");
                _tempText = _textMeshProUGUI.text.Replace(_word.Title, "<b><link>"+_word.Title+ "</link></b>");
                _textMeshProUGUI.SetText(_tempText);
                Debug.Log("add one word");
            }
            _textMeshProUGUI.UpdateFontAsset();
            _textMeshProUGUI.ForceMeshUpdate();
        }

        public bool ShouldDisplayTooltip(int linkTaggedText, out HoverInfo keyWord)
        {
            if (!_isActive || _tempLinkTaggedText != linkTaggedText)
            {
                _tempLinkTaggedText = linkTaggedText;
                TMP_LinkInfo linkInfo = _textMeshProUGUI.textInfo.linkInfo[linkTaggedText];

                if (!hoverDictionary.SearchInDictionary(linkInfo.GetLinkText(), out keyWord))
                    return false;
                
                _isActive = true;
                return true;
            }

            keyWord = default;
            return false;
        }

        public void Deactivate()
        {
            IsActive = false;
        }
    }
}