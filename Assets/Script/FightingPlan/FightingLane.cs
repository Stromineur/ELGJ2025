using Script.Words;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Script.FightingPlan
{
    public class FightingLane : MonoBehaviour
    {
        [SerializeField] private Transform allyPosition;
        [SerializeField] private Transform enemyPosition;

        public void Spawn(IFightingData fightingData, Transform parent)
        {
            bool ally = fightingData is WordData;
            Transform position = ally ? allyPosition : enemyPosition;
            FightingWord word = Instantiate(fightingData.Prefab, position.position, Quaternion.identity, parent);
            word.Init(fightingData);
        }
        
        [Button]
        public void SpawnAlly(WordData wordData)
        {
            Spawn(wordData, transform);
        }
        
        [Button]
        public void SpawnEnemy(BadWordData badWordData)
        {
            Spawn(badWordData, transform);
        }
    }
}
