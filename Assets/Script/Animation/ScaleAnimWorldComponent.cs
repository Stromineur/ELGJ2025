using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NecroMotMicon.Script.Animation
{
    public class ScaleAnimWorldComponent : MonoBehaviour
    {
        [SerializeField] private float scaleFactor = 1f;
        [SerializeField] private float scaleTime = 1f;

        private Vector3 initialScale;
        private Vector3 targetScale;
        private Tween pulseTween;
        private bool canScale = true;

        private void Awake()
        {
            if (transform.localScale == Vector3.zero)
            {
                initialScale = new  Vector3(1, 1, 1);
            }
            else
            {
                initialScale = transform.localScale;
            }
        }

        [Button(ButtonSizes.Large)]
        public void ScaleDownThenDisable()
        {
            transform.DOScale(Vector3.zero, scaleTime).SetEase(Ease.InBack).OnComplete(() => gameObject.SetActive(false));
        }
        
        [Button(ButtonSizes.Large)]
        public void EnableThanScaleUp()
        {
            gameObject.SetActive(true);
            targetScale = initialScale * scaleFactor;
            transform.DOScale(targetScale, scaleTime).SetEase(Ease.OutBack);
        }

        [Button(ButtonSizes.Large)]
        public void ScaleUp()
        {
            targetScale = initialScale * scaleFactor;
            transform.DOScale(targetScale, scaleTime).SetEase(Ease.OutBack);
        }
        
        [Button(ButtonSizes.Large)]
        public void PulseScale()
        {
            if (canScale)
            {
                canScale = false;
                targetScale = initialScale * scaleFactor;
                pulseTween = transform.DOScale(targetScale, scaleTime).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
            }
        }

        [Button(ButtonSizes.Large)]
        public void StopPulseScale()
        {
            pulseTween.Kill();
            ResetScale();
            canScale = true;
        }

        [Button(ButtonSizes.Large)]
        public void ResetScale()
        {
            transform.DOScale(initialScale, scaleTime).SetEase(Ease.OutBack);
        }
    }
}
