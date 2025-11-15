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
        [SerializeField] private List<GameObject> scaledGO;
        [SerializeField] private GameObject hpGo;
        [SerializeField] private GameObject bookGO;
        [SerializeField] private GameObject heartGO;
        [SerializeField] private float scaleFactor = 1f;
        [SerializeField] private float scaleTime = 1f;
        [SerializeField] private int loopNumber = 1;
        [SerializeField] private DamageNumber hpLossValue;

        private TMP_Text hpText;
        private PlayerArea _playerArea;
        private Vector3 initialScale;
        private Vector3 targetScale;
        private Color initialBookColor;
        private Tween hpPulseTween;
        private Tween bookPulseTween;
        private Tween bookColorTween;
        private Tween heartTween;
        private bool canScale = true;
        private bool isInitEnded  = false;

        private void Awake()
        {
            if (transform.localScale == Vector3.zero) initialScale = new Vector3(1, 1, 1); else initialScale = transform.localScale;
            initialBookColor = bookGO.GetComponent<SpriteRenderer>().color;
            hpText = hpGo.GetComponent<TMP_Text>();
            _playerArea = ServiceLocator.Instance.PlayerArea;
            UpdateHpDisplay(_playerArea.Hp);
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
        
        // Ajouter un fonction feedback pour la prise de dégâts
        // 1. Scale yoyo bouquin + texte
        // 2. Cligotement en rouge nécromomancien + bouquin + texte
        // 3. Animation de dodo sur le nécromomancien sur la prise de dégâts ?
        
        [Button(ButtonSizes.Large)]
        public void PulseScale()
        {
            if (canScale && isInitEnded)
            {
                canScale = false;
                targetScale = initialScale * scaleFactor;
                hpPulseTween = hpGo.transform.DOScale(targetScale, scaleTime).SetEase(Ease.InOutSine).SetLoops(loopNumber, LoopType.Yoyo).OnComplete(() => StopPulseScale());
                bookPulseTween = bookGO.transform.DOScale(targetScale, scaleTime).SetEase(Ease.InOutSine).SetLoops(loopNumber, LoopType.Yoyo).OnComplete(() => StopPulseScale());
                heartTween = heartGO.transform.DOScale(targetScale, scaleTime).SetEase(Ease.InOutSine).SetLoops(loopNumber, LoopType.Yoyo).OnComplete(() => StopPulseScale());
                bookColorTween = bookGO.GetComponent<SpriteRenderer>().DOColor(Color.red, scaleTime).SetEase(Ease.InOutSine).SetLoops(loopNumber, LoopType.Yoyo);
                hpLossValue.Spawn(transform.position, -1);
            }
            
            isInitEnded = true;
        }

        [Button(ButtonSizes.Large)]
        public void StopPulseScale()
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
            hpGo.transform.DOScale(initialScale, scaleTime).SetEase(Ease.OutBack);
            bookGO.transform.DOScale(initialScale, scaleTime).SetEase(Ease.OutBack);
            heartGO.transform.DOScale(initialScale, scaleTime).SetEase(Ease.OutBack);
            bookGO.GetComponent<SpriteRenderer>().color = initialBookColor;
        }
        
    }
}
