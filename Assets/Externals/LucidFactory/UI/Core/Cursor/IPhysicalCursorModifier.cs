using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public interface IPhysicalCursorModifier : ICursorModifier, IPointerEnterHandler, IPointerExitHandler
{
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        SetCursor();
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        RemoveCursor();
    }
}
