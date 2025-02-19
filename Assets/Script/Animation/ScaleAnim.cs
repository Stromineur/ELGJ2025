using System;
using System.ComponentModel;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
public class ScaleAnim : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float defaultSize;
    [SerializeField] private float scaledSize;
    [SerializeField] private float transitionSpeed;
    
    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        rectTransform.DOScale(new Vector3(scaledSize, scaledSize, scaledSize),transitionSpeed);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        rectTransform.DOScale(new Vector3(defaultSize, defaultSize, defaultSize),transitionSpeed);
    }
}
