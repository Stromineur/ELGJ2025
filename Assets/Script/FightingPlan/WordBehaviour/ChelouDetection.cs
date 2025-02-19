using System;
using Script.Core;
using UnityEngine;

namespace Script.FightingPlan.WordBehaviour
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
            
            RaycastHit2D enemy = Physics2D.Raycast(transform.position, new Vector2(0, 1), -1 + _badWord.Speed * GameController.GameMetrics.SpeedMultiplier * Time.deltaTime * 10, _badWord.EnemyMask);

            if (enemy)
            {
                _lineChanger.ChangeLine();
                _changeLineRemaining--;
            }
        }
    }
}
