using LTX.ChanneledProperties.Priorities;
using LucidFactory.UI.Externals.LucidFactory.UI.Core.Cursor;
using UnityEngine;
using UnityEngine.EventSystems;

public class UICursorModifier : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] protected CursorIcon hoverCursor;
    [SerializeField] protected PriorityTags priority;

    private void OnDisable()
    {
        StopDisplayCursor();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        DisplayCursor();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopDisplayCursor();
    }

    protected virtual void DisplayCursor()
    {
        CursorManager.Icon.AddPriority(this, priority, hoverCursor);
    }

    protected virtual void StopDisplayCursor()
    {
        CursorManager.Icon.RemovePriority(this);
    }
}
