using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Inu/CursorIcon")]
public class CursorIcon : ScriptableObject
{
    public Texture2D Texture;
    public CursorMode CursorMode;
    public Vector2 HotSpot;
}