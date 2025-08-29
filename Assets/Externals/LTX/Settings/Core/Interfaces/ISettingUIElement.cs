using LTX.Settings.UI.Internal;

namespace LTX.Settings.UI
{
    public interface ISettingUIElement<T> where T : SettingsUIBuilder
    {
        public T UIBuilder { get; set; }
    }
}
