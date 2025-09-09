using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NecroMotMicon.Script.Animation
{
    public class ScaleAnimWorldComponent : MonoBehaviour
    {
        [SerializeField] private float scaleFactor = 1f;
        [SerializeField] private float scaleTime = 1f;
        
        [Button(ButtonSizes.Large)]
        public void ScaleDownThenDisable()
        {
            transform.DOScale(Vector3.zero, scaleTime).SetEase(Ease.InBack).OnComplete(() => gameObject.SetActive(false));
        }
        
        [Button(ButtonSizes.Large)]
        public void EnableThanScaleUp()
        {
            gameObject.SetActive(true);
            transform.DOScale(scaleFactor, scaleTime).SetEase(Ease.OutBack);
        }
        
    }
}
