using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using LTX;
using LTX.Singletons;
using LucidFactory.UI;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Untold.Core.Stories.UI
{
    public class HoverManager : MonoSingleton<HoverManager, HoverManagerFactory>
    {
        [SerializeField] private Tooltip _tooltipPrefab;

        private Canvas _canvas;
        private Tooltip _tooltip;
        private List<HoverText> hoverTexts = new();
        public Camera Camera => _canvas.worldCamera;
        public bool shouldHover = true;

        public void SetCanvas([NotNull] Canvas canvas)
        {
            _canvas = canvas;
            
            CreateTooltip();
        }

        public void OnMouseMoved(InputAction.CallbackContext obj)
        {
            DisplayHoverTooltip();
        }

        /// <summary>
        /// Instantiation of tooltip prefab
        /// </summary>
        /// <returns>Tooltip Prefab</returns>
        public void CreateTooltip()
        {
            if (_tooltip != null) return;
            Tooltip tooltip = Instantiate(_tooltipPrefab);
            tooltip.transform.SetParent(_canvas.transform, false);
            tooltip.gameObject.SetActive(false);
            _tooltip = tooltip;
        }

        /// <summary>
        /// Set tooltip active with an ObjectToHover to setUp values
        /// </summary>
        /// <param name="keyWord"></param>
        public void DisplayTooltip(HoverInfo keyWord)
        {
            if(!shouldHover)
                return;
            
            if(_tooltip == null)
            {
                CreateTooltip();
            }
            
            _tooltip.SetTooltipFromWord(keyWord);
            _tooltip.transform.SetAsLastSibling();

            _tooltip.transform.position = _tooltip.DefinePositionInCanvas(_canvas);
            Canvas.ForceUpdateCanvases();
            _tooltip.gameObject.SetActive(true);
        }

        public void AddHover(HoverText text)
        {
            hoverTexts.Add(text);
        }

        public void RemoveHover(HoverText text)
        {
            hoverTexts.Remove(text);
            if (text.IsActive)
            {
                text.Deactivate();
                UnDisplayTooltip();
            }
        }

        private void DisplayHoverTooltip()
        {
            if(hoverTexts == null)
                return;

            foreach (HoverText hoverText in hoverTexts)
            {
                int linkTaggedText = TMP_TextUtilities.FindIntersectingLink(hoverText._textMeshProUGUI, Input.mousePosition, Camera);
                if (linkTaggedText != -1)
                {
                    Debug.Log("found link");
                    if(hoverText.ShouldDisplayTooltip(linkTaggedText, out HoverInfo keyWord))
                        DisplayTooltip(keyWord);
                }
                else if (hoverText.IsActive)
                {
                    hoverText.Deactivate();
                    UnDisplayTooltip();
                }
            }
        }

        /// <summary>
        /// Set tooltip inactive
        /// </summary>
        public void UnDisplayTooltip()
        {
            if (_tooltip != null)
            {
                _tooltip.gameObject.SetActive(false);
            }
        }

        protected override void OnExistingInstanceFound(HoverManager existingInstance)
        {
            Debug.LogWarning("Existing instance found");
            Destroy(this);
        }
    }

}