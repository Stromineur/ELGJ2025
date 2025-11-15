using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace NecroMotMicon
{
    public class VariousWordAnim : MonoBehaviour
    {
        [SerializeField] private GameObject affectedObject;
        [SerializeField] private SpriteRenderer lockObject;
        [SerializeField] private SpriteRenderer lockBackground;

        [Header("Unlock parameters")] [SerializeField]
        private float shakeDuration = 1f;

        [SerializeField] private float shakeStrengh = 1f;
        [SerializeField] private AnimationCurve shakeCurve;
        [SerializeField] private Color backGroundShakeColor;

        [Header("Not possible parameters")]
        [SerializeField] private Color effectColor;
        [SerializeField] private float effectDuration;
        [SerializeField] private int flashNumber;
        
        private Dictionary<Object, Color> baseColors = new();
        private Vector3 initialPosition;
        private bool canShake = true;

        [Button(ButtonSizes.Large)]
        public void Unlock()
        {
            if (canShake)
            {
                canShake = false;
                StartCoroutine(Shaking());
                lockBackground.DOColor(backGroundShakeColor, shakeDuration);
            }
        }

        IEnumerator Shaking()
        {
            initialPosition = affectedObject.transform.position;
            float elapsedTime = 0f;

            while (elapsedTime < shakeDuration)
            {
                elapsedTime += Time.deltaTime;
                float strengh = shakeCurve.Evaluate(elapsedTime / shakeDuration);
                affectedObject.transform.position =
                    initialPosition + Random.insideUnitSphere * ((strengh * shakeStrengh) / 1000);
                yield return null;
            }

            FadeOutAndDisable();
            affectedObject.transform.position = initialPosition;
            canShake = true;
        }

        private void FadeOutAndDisable()
        {
            lockObject.DOFade(0f, shakeDuration);
            lockBackground.DOFade(0f, shakeDuration).OnComplete(() => affectedObject.SetActive(false));
        }

        [Button(ButtonSizes.Large)]
        public void NotPossible()
        {
            foreach (SpriteRenderer spriteRenderer in GetComponentsInChildren<SpriteRenderer>())
            {
                SaveBaseColors(spriteRenderer, spriteRenderer.color);
                spriteRenderer.DOColor(effectColor, effectDuration).SetLoops(flashNumber * 2, LoopType.Yoyo).OnComplete(() => spriteRenderer.DOColor(baseColors[spriteRenderer], effectDuration));
            }
            
            foreach (TextMeshPro tmp in GetComponentsInChildren<TextMeshPro>())
            {
                SaveBaseColors(tmp, tmp.color);
                tmp.DOColor(effectColor, effectDuration).SetLoops(flashNumber * 2, LoopType.Yoyo).OnComplete(() => tmp.DOColor(baseColors[tmp], effectDuration));
            }
        }

        private void SaveBaseColors(Object key, Color color)
        {
            if (!baseColors.ContainsKey(key))
            {
                baseColors[key] = color;
            }
        }
    }
}
