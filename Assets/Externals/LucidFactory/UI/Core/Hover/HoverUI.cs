using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Untold.Core.Stories.UI
{
    public class HoverUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public event Action<HoverInfo> OnHoverInfoChanged;
        
        [SerializeField] private HoverInfo hoverInfo;
        private bool _isActive = false;

        public void SetHoverInfo(HoverInfo hoverInfo)
        {
            this.hoverInfo = hoverInfo;
            OnHoverInfoChanged?.Invoke(this.hoverInfo);
        }
        
        /// <summary>
        /// On Mouse cursor over the object, display tooltip if not active
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!_isActive)
            {
                HoverManager.Instance.DisplayTooltip(hoverInfo);
                _isActive = true;
            }
        }

        /// <summary>
        /// On Mouse cursor ends over the object, unShow tooltip and make isActive false
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerExit(PointerEventData eventData)
        {
            if (_isActive)
            {
                _isActive = false;
                HoverManager.Instance.UnDisplayTooltip();
            }
        }
    }
}