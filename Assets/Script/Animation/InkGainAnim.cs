using System;
using System.Collections;
using DG.Tweening;
using Script.Core;
using TMPro;
using Unity.Collections;
using UnityEngine;

namespace NecroMotMicon
{
    public class InkGainAnim : MonoBehaviour
    {
        [Header("Text counter")]
        [SerializeField] private TextMeshPro inkText;
        [SerializeField] private GameObject inkDropletGameObject;
        [SerializeField] private int CountsFPS = 30;
        [SerializeField] private float duration = 1f;
        [SerializeField] private string numberFormat = "N0";

        [Header("Scale parameters")]
        [SerializeField] private float scaleFactor = 1f;
        [SerializeField] private float scaleBuffer = 1f; //to match the object scale with font size scale
        [SerializeField] private float scaleSpeed = 1f;
        
        private int _value;
        private bool canScale = true;
        private float baseFontSize;
        private RectTransform rectTransform;
        private Vector3 initialScale;
        private Vector3 targetScale;
        private Tween pulseInkTween;
        private Tween pulseTextTween;

        public int Value
        {
            get{return _value;}
            set
            {
                UpdateText(value);
                _value = value;
            }
        }
        
        private Coroutine CountingCoroutine;

        private void Awake()
        {
            _value = GameController.GameMetrics.StartInk;
            rectTransform = inkDropletGameObject.GetComponent<RectTransform>();
            initialScale = rectTransform.localScale;
            baseFontSize = inkText.fontSize;
        }
        
        private void UpdateText(int newValue)
        {
            StopPulseScale();
            StopAllCoroutines();
            PulseScale();
            StartCoroutine(CountText(newValue));
            
        }
        
        
        IEnumerator CountText(int newValue)
        {
            WaitForSeconds wait = new WaitForSeconds(1f / CountsFPS);
            int previousValue = _value;
            int stepAmount;

            if (newValue - previousValue < 0)
            {
                stepAmount = Mathf.FloorToInt((newValue - previousValue) / (CountsFPS * duration));
            }
            else
            {
                stepAmount = Mathf.CeilToInt((newValue - previousValue) / (CountsFPS * duration));
            }

            if (previousValue < newValue)
            {
                while (previousValue < newValue)
                {
                    previousValue += stepAmount;
                    if (previousValue > newValue)
                    {
                        previousValue = newValue;
                    }
                    
                    inkText.SetText(previousValue.ToString(numberFormat));
                    
                    yield return wait;
                }
            }
            else
            {
                while (previousValue > newValue)
                {
                    previousValue += stepAmount;
                    
                    if (previousValue < stepAmount)
                    {
                        previousValue = newValue;
                    }
                    
                    inkText.SetText(previousValue.ToString(numberFormat));
                    
                    yield return wait;
                }
            }
            
            StopPulseScale();
        }
        
        
        private void PulseScale()
        {
            if (canScale)
            {
                canScale = false;
                targetScale = rectTransform.localScale * scaleFactor;
                pulseInkTween = rectTransform.DOScale(targetScale, scaleSpeed).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
                pulseTextTween = DOTween.To(() => inkText.fontSize, x => inkText.fontSize = x, baseFontSize * scaleFactor * scaleBuffer, scaleSpeed).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
            }
        }

        private void StopPulseScale()
        {
            pulseInkTween.Kill();
            pulseTextTween.Kill();
            ResetScale();
            canScale = true;
        }
        
        private void ResetScale()
        {
            rectTransform.DOScale(initialScale, scaleSpeed / 2).SetEase(Ease.OutBack);
            DOTween.To(() => inkText.fontSize, x => inkText.fontSize = x, baseFontSize, scaleSpeed).SetEase(Ease.InOutSine);
        }
    }
}
