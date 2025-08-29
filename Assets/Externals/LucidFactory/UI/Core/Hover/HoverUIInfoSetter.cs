using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Untold.Core.Stories;
using Untold.Core.Stories.UI;

[RequireComponent(typeof(HoverUI))]
public class HoverUIInfoSetter : MonoBehaviour
{
    [SerializeField] private HoverDictionaryData hoverDictionaryData;
    private HoverUI _hoverUI;
    private IHoverUIInfoProvider _infoProvider;
    
    private void Awake()
    {
        _hoverUI = GetComponent<HoverUI>();
        _infoProvider = GetComponent<IHoverUIInfoProvider>();
    }

    private void OnEnable()
    {
        _infoProvider.OnHoverProviderChanged += SetHoverInfo;
    }

    private void OnDisable()
    {
        _infoProvider.OnHoverProviderChanged -= SetHoverInfo;
    }

    public void SetHoverInfo(string title)
    {
        if(hoverDictionaryData.SearchInDictionary(title, out HoverInfo hoverInfo))
            _hoverUI.SetHoverInfo(hoverInfo);
        else 
            Debug.LogWarning("No Hover Info was found for " + gameObject.name);
    }
}
