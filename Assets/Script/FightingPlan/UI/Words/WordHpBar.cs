using System;
using UnityEngine;
using UnityEngine.UI;

namespace NecroMotMicon.Script.FightingPlan.UI.Words
{
    public class WordHpBar : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Image _image;
        [SerializeField] private Sprite _redHpBar;
        private FightingWord _fightingWord;
        private bool _redBar;

        private void Awake()
        {
            _fightingWord = GetComponentInParent<FightingWord>();
            _canvas.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            _fightingWord.OnHpChanged += UpdateUiHp;
        }

        private void OnDisable()
        {
            _fightingWord.OnHpChanged -= UpdateUiHp;
        }

        private void UpdateUiHp(float currentHp, float maxHp)
        {
            _canvas.gameObject.SetActive(true);
            _image.fillAmount = currentHp / maxHp;
            
            if(!_redBar && currentHp <= maxHp / 2)
            {
                _image.sprite = _redHpBar;
                _redBar = true;
            }
        }
    }
}
