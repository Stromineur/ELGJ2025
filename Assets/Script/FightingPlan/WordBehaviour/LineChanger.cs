using System;
using DG.Tweening;
using UnityEngine;

namespace Script.FightingPlan.WordBehaviour
{
    public class LineChanger : MonoBehaviour
    {
        private FightingWord _fightingWord;

        private void Awake()
        {
            _fightingWord = GetComponentInParent<FightingWord>();
        }

        public void ChangeLine()
        {
            FightingLane adjacentLane = _fightingWord.FightingLane.GetAdjacentLane();
            ChangeLine(adjacentLane);
        }

        public void ChangeLine(FightingLane fightingLane)
        {
            _fightingWord.MoveLane(fightingLane);
        }
    }
}
