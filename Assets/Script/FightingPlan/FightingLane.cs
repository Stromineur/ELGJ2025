using System.Collections.Generic;
using Script.Words;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Script.FightingPlan
{
    public class FightingLane : MonoBehaviour
    {
        public FightingLane PreviousLane => previousLane;
        public FightingLane NextLane => nextLane;
        
        [SerializeField] private Transform allyPosition;
        [SerializeField] private Transform enemyPosition;
        [SerializeField] private FightingLane previousLane;
        [SerializeField] private FightingLane nextLane;

        public List<BadWord> BadWords { get; private set; } = new();
        public List<PreciousWord> PreciousWords { get; private set; } = new();

        public FightingWord Spawn(IFightingData fightingData, Transform parent)
        {
            bool ally = fightingData is WordData;
            Transform position = ally ? allyPosition : enemyPosition;
            return Spawn(fightingData, parent, position.position);
        }

        public FightingWord Spawn(IFightingData fightingData, Transform parent, Vector2 position)
        {
            FightingWord word = Instantiate(fightingData.Prefab, position, Quaternion.identity, parent ? parent : transform);
            word.Init(fightingData, this);

            if (word is BadWord badWord)
                BadWords.Add(badWord);
            else if(word is PreciousWord preciousWord)
                PreciousWords.Add(preciousWord);
            
            word.OnDeath += OnWordDeath;
            
            return word;
        }

        private void OnWordDeath(FightingWord killed, FightingWord _)
        {
            killed.OnDeath -= OnWordDeath;
            if (killed is BadWord badWord)
                BadWords.Remove(badWord);
            else if(killed is PreciousWord preciousWord)
                PreciousWords.Remove(preciousWord);
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

        public FightingLane GetAdjacentLane()
        {
            if (!previousLane && nextLane)
                return nextLane;
            if (!nextLane && previousLane)
                return nextLane;
            if (previousLane && nextLane)
            {
                int rnd = Random.Range(0, 2);

                return rnd == 0 ? nextLane : previousLane;
            }

            return null;
        }
    }
}
