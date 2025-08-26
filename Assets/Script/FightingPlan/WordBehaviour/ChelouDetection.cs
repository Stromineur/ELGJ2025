using System;
using Script.Core;
using UnityEngine;

namespace NecroMotMicon.Script.FightingPlan.WordBehaviour
{
    [RequireComponent(typeof(LineChanger))]
    public class ChelouDetection : MonoBehaviour
    {
        private BadWord _badWord;
        private LineChanger _lineChanger;
        private int _changeLineRemaining;

        private void Awake()
        {
            _badWord = GetComponentInParent<BadWord>();
            _lineChanger = GetComponent<LineChanger>();
            _changeLineRemaining = 1;
        }

        private void Update()
        {
            if (_changeLineRemaining <= 0)
                return;

            RaycastHit2D enemy = Physics2D.Raycast(transform.position, new Vector2(1, 0), _badWord.GetRaycastDistance() * 1.5f, _badWord.EnemyMask);

            if (enemy)
            {
                _lineChanger.ChangeLine();
                _changeLineRemaining--;
            }
        }
    }
}
