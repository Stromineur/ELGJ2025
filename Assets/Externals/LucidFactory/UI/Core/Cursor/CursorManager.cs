using LTX.ChanneledProperties;
using LTX.ChanneledProperties.Priorities;
using UnityEngine;

namespace LucidFactory.UI.Externals.LucidFactory.UI.Core.Cursor
{
    public static class CursorManager
    {
        public static Priority<bool> IsVisible { get; private set; }
        public static Priority<CursorLockMode> LockMode { get; private set; }
        public static Priority<CursorIcon> Icon { get; private set; }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Init()
        {
            IsVisible = new Priority<bool>(true);
            IsVisible.AddOnValueChangeCallback(isVisible => UnityEngine.Cursor.visible = isVisible, true);
            CursorLockMode lockMode = CursorLockMode.Confined;
#if UNITY_EDITOR
            lockMode = CursorLockMode.None;
#endif
            LockMode = new Priority<CursorLockMode>(lockMode);
            LockMode.AddOnValueChangeCallback(lockMode => UnityEngine.Cursor.lockState = lockMode, true);
            Icon = new Priority<CursorIcon>(Resources.Load<CursorIcon>("Cursors/DefaultCursor"));
            Icon.AddOnValueChangeCallback(icon => { UnityEngine.Cursor.SetCursor(icon.Texture, icon.HotSpot, icon.CursorMode); }, true);
        }

        public static void Clear()
        {
            IsVisible.Clear();
            LockMode.Clear();
            Icon.Clear();
        }
    }
}
