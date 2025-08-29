using System;
using System.Collections;
using System.Collections.Generic;
using LucidFactory.UI.Externals.LucidFactory.UI.Core.Cursor;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class ButtonCursorModifier : UICursorModifier
{
    [SerializeField, PropertyOrder(-1)] private Selectable button;
    [SerializeField] private CursorIcon cantHoverCursor;

    private void Awake()
    {
        if(button == null)
            button = GetComponent<Selectable>();
    }

    protected override void DisplayCursor()
    {
        if(button.interactable)
            base.DisplayCursor();
        else 
            CursorManager.Icon.AddPriority(this, priority, cantHoverCursor);
    }
}
