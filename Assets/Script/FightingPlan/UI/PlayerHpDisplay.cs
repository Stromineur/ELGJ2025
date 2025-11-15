using System.Collections.Generic;
using Script.Core;
using TMPro;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;
using DamageNumbersPro;

namespace NecroMotMicon.Script.FightingPlan.UI
{
    public class PlayerHpDisplay : MonoBehaviour
    {
        [Header("Gameobjects")]
        // Hp changes
        [SerializeField] private GameObject hpGo;
        private TMP_Text hpText;
        private Vector3 hpInitialScale;
        private Tween hpPulseTween;
        
        // Book changes
        [SerializeField] private GameObject bookGO;
        private Vector3 bookInitialScale;
        private Color initialBookColor; 
        private Tween bookPulseTween;
        private Tween bookColorTween;
        
        // Heart icon changes
        [SerializeField] private GameObject heartGO;
        private Vector3 heartInitialScale;
        private Tween heartTween;
        
        [Header("Parameters")]
        [SerializeField] private float scaleFactor = 1f;
        [SerializeField] private float scaleTime = 1f;
        [SerializeField] private int loopNumber = 1;
        [SerializeField] private DamageNumber hpLossValue;

        
        private PlayerArea _playerArea;
        private bool canScale = true;
        private bool isInitEnded  = false;
        
        
        private void Awake()
        {
            // Set les valeurs de bases
            hpInitialScale = hpGo.transform.localScale;
            bookInitialScale = bookGO.transform.localScale;
            heartInitialScale = heartGO.transform.localScale;
            initialBookColor = bookGO.GetComponent<SpriteRenderer>().color;
            
            hpText = hpGo.GetComponent<TMP_Text>();
            _playerArea = ServiceLocator.Instance.PlayerArea;
            hpText.text = _playerArea.Hp.ToString();
        }

        private void OnEnable()
        {
            _playerArea.OnDamageTaken += UpdateHpDisplay;
        }

        private void OnDisable()
        {
            _playerArea.OnDamageTaken -= UpdateHpDisplay;
        }

        private void UpdateHpDisplay(float obj)
        {
            hpText.text = obj.ToString();
            PulseScale();
        }
        
        [Button(ButtonSizes.Large)]
        public void PulseScale()
        {
            if (canScale && isInitEnded)
            {
                canScale = false;
                hpPulseTween = hpGo.transform.DOScale(hpInitialScale * scaleFactor, scaleTime).SetEase(Ease.InOutSine).SetLoops(loopNumber, LoopType.Yoyo).OnComplete(() => StopTweens());
                bookPulseTween = bookGO.transform.DOScale(bookInitialScale * scaleFactor, scaleTime).SetEase(Ease.InOutSine).SetLoops(loopNumber, LoopType.Yoyo);
                heartTween = heartGO.transform.DOScale(heartInitialScale * scaleFactor, scaleTime).SetEase(Ease.InOutSine).SetLoops(loopNumber, LoopType.Yoyo);
                bookColorTween = bookGO.GetComponent<SpriteRenderer>().DOColor(Color.red, scaleTime).SetEase(Ease.InOutSine).SetLoops(loopNumber, LoopType.Yoyo);
                hpLossValue.Spawn(transform.position, -1);
            }
            
            isInitEnded = true;
        }

        [Button(ButtonSizes.Large)]
        public void StopTweens()
        {
            hpPulseTween.Kill();
            bookPulseTween.Kill();
            heartTween.Kill();
            bookColorTween.Kill();
            ResetScaleEndColor();
            canScale = true;
        }

        [Button(ButtonSizes.Large)]
        public void ResetScaleEndColor()
        {
            hpGo.transform.DOScale(hpInitialScale, scaleTime).SetEase(Ease.OutBack);
            bookGO.transform.DOScale(bookInitialScale, scaleTime).SetEase(Ease.OutBack);
            heartGO.transform.DOScale(heartInitialScale, scaleTime).SetEase(Ease.OutBack);
            bookGO.GetComponent<SpriteRenderer>().color = initialBookColor;
        }
        
    }
}
