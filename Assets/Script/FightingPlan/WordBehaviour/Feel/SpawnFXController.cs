using DG.Tweening;
using UnityEngine;

namespace NecroMotMicon.Script.FightingPlan.WordBehaviour.Feel
{
    public class SpawnFXController : MonoBehaviour
    {
        [SerializeField] private Transform fxObject;

        private FightingWord fightingWord;
        
        private void Awake()
        {
            fightingWord = GetComponentInParent<FightingWord>();
            
            transform.parent = fightingWord.transform.parent;
            transform.localScale = Vector3.zero;
            fxObject.DOScale(1, 0.3f).SetTarget(this);
        }

        private void OnEnable()
        {
            fightingWord.OnInitialized += RemoveFX;
        }

        private void OnDisable()
        {
            fightingWord.OnInitialized -= RemoveFX;
        }

        private void RemoveFX()
        {
            if (DOTween.IsTweening(this))
            {
                DOTween.Kill(this);
                transform.localScale = Vector3.one;
            }
            
            fxObject.DOScale(0, 0.5f)
                .OnComplete(() => Destroy(gameObject))
                .SetTarget(this);
        }
    }
}
