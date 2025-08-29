using System.Collections;
using System.Collections.Generic;
using LTX.ChanneledProperties;
using LTX.ChanneledProperties.Priorities;
using UnityEngine;

public interface ICursorModifier
{
    GameObject SelfGo { get; }
    CursorIcon CursorIcon { get; }
    PriorityTags PriorityTag { get; }

    public void SetCursor();
    
    protected void RemoveCursor();
}
