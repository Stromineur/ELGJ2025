namespace LTX.Settings
{

    public interface ISettingProvider
    {
        public bool TryReadSetting(ref ISetting setting);
        public bool TryWriteSetting(ref ISetting setting);
    }
}
