using UnityEngine;

namespace Script.FightingPlan.WordBehaviour
{
    [RequireComponent(typeof(LineChanger))]
    public class EnigmatiqueDetection : MonoBehaviour
    {
        private PreciousWord _preciousWord;
        private LineChanger _lineChanger;
        private float _timeBeforeNextChangeLine;

        private void Awake()
        {
            _preciousWord = GetComponentInParent<PreciousWord>();
            _lineChanger = GetComponent<LineChanger>();
            _timeBeforeNextChangeLine = 0;
        }

        private void Update()
        {
            if (_timeBeforeNextChangeLine > 0)
            {
                _timeBeforeNextChangeLine -= Time.deltaTime;
                return;
            }

            float closestEnemy = Mathf.Infinity;
            FightingLane moveToLane = null;

            FightingLane previousLane = _preciousWord.FightingLane.PreviousLane;
            if (previousLane)
            {
                foreach (BadWord badWord in previousLane.BadWords)
                {
                    if (badWord.transform.position.y > transform.position.y)
                    {
                        float distance = Vector2.Distance(badWord.transform.position, transform.position);
                        if (distance < closestEnemy)
                        {
                            moveToLane = previousLane;
                            closestEnemy = distance;
                        }
                    }
                }
            }

            FightingLane nextLane = _preciousWord.FightingLane.NextLane;
            if (nextLane)
            {
                foreach (BadWord badWord in nextLane.BadWords)
                {
                    if (badWord.transform.position.y > transform.position.y)
                    {
                        float distance = Vector2.Distance(badWord.transform.position, transform.position);
                        if (distance < closestEnemy)
                        {
                            moveToLane = previousLane;
                            closestEnemy = distance;
                        }
                    }
                }
            }

            if (moveToLane != null)
            {
                _lineChanger.ChangeLine(moveToLane);
                _timeBeforeNextChangeLine = 0.5f;
            }
        }
    }
}
