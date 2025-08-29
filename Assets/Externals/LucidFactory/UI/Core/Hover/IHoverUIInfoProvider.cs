using System;

public interface IHoverUIInfoProvider
{
    public event Action<string> OnHoverProviderChanged;
}
