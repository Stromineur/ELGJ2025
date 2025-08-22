using UnityEngine;

namespace NecroMotMicon.Script.FightingPlan.WordBehaviour
{
    [RequireComponent(typeof(LineChanger))]
    public class EnigmatiqueDetection : MonoBehaviour
    {
        private PreciousWord _preciousWord;
        private LineChanger _lineChanger;
        private float _timeBeforeNextChangeLine;
        
        private float _distance;
        private float _closestEnemy;

        private void Awake()
        {
            _preciousWord = GetComponentInParent<PreciousWord>();
            _lineChanger = GetComponent<LineChanger>();
            _timeBeforeNextChangeLine = 0;
        }

        private void Update()
        {
            if (!_preciousWord.IsInitialized)
                return;
            
            if (_timeBeforeNextChangeLine > 0)
            {
                _timeBeforeNextChangeLine -= Time.deltaTime;
                return;
            }

            _closestEnemy = Mathf.Infinity;
            FightingLane moveToLane = null;
            
            foreach (BadWord badWord in _preciousWord.FightingLane.BadWords)
            {
                if (!IsEnemyCloser(badWord)) 
                    continue;
                
                _closestEnemy = _distance;
            }

            FightingLane previousLane = _preciousWord.FightingLane.LeftLane;
            if (previousLane)
            {
                foreach (BadWord badWord in previousLane.BadWords)
                {
                    if (!IsEnemyCloser(badWord)) 
                        continue;
                    
                    moveToLane = previousLane;
                    _closestEnemy = _distance;
                }
            }

            FightingLane nextLane = _preciousWord.FightingLane.RightLane;
            if (nextLane)
            {
                foreach (BadWord badWord in nextLane.BadWords)
                {
                    if (!IsEnemyCloser(badWord)) 
                        continue;
                    
                    moveToLane = nextLane;
                    _closestEnemy = _distance;
                }
            }

            if (moveToLane)
            {
                _lineChanger.ChangeLine(moveToLane);
                _timeBeforeNextChangeLine = 1f;
            }
        }

        private bool IsEnemyCloser(BadWord badWord)
        {
            if (badWord && badWord.transform.position.y > transform.position.y)
            {
                _distance = badWord.transform.position.y - transform.position.y;
                if (_distance < _closestEnemy)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
