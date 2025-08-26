using System;
using UnityEngine;

namespace NecroMotMicon.Script.FightingPlan.Feedback
{
    public class GraveFreeExhuming : MonoBehaviour
    {
        [SerializeField] private GameObject freeExhumingTag;

        private void Awake()
        {
            StopDisplayFreeExhuming();
        }

        private void OnEnable()
        {
            LaneManager.Instance.OnFreeExhuming += DisplayFreeExhuming;
            LaneManager.Instance.OnNotFreeExhuming += StopDisplayFreeExhuming;
        }

        private void OnDrawGizmos()
        {
            LaneManager.Instance.OnFreeExhuming -= DisplayFreeExhuming;
            LaneManager.Instance.OnNotFreeExhuming -= StopDisplayFreeExhuming;
        }

        private void DisplayFreeExhuming()
        {
            freeExhumingTag.SetActive(true);
        }

        private void StopDisplayFreeExhuming()
        {
            freeExhumingTag.SetActive(false);
        }
    }
}
