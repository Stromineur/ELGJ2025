using Script.Core;
using UnityEngine;

namespace Script.FightingPlan.WordBehaviour
{
    public class CourrielWritingFinish : MonoBehaviour
    {
        private PreciousWord _preciousWord;

        private void Awake()
        {
            _preciousWord = GetComponentInParent<PreciousWord>();
        }

        private void OnEnable()
        {
            _preciousWord.OnReachedEndEvent += RemoveCurrentWritingTime;
        }

        private void OnDisable()
        {
            _preciousWord.OnReachedEndEvent -= RemoveCurrentWritingTime;
        }

        private void RemoveCurrentWritingTime(FightingWord fightingWord)
        {
            ServiceLocator.Instance.WordManager.EndCurrentWriting();
        }
    }
}
