using System;
using System.Collections.Generic;
using Script.Words;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Script.FightingPlan
{
    public class FightingLane : MonoBehaviour
    {
        public bool CanSpawn => (!lastPreciousWord || !lastPreciousWord.IsInitialized) && _canSpawn;
        public FightingLane PreviousLane => previousLane;
        public FightingLane NextLane => nextLane;
        
        [SerializeField] private Transform allyPosition;
        [SerializeField] private Transform enemyPosition;
        [SerializeField] private FightingLane previousLane;
        [SerializeField] private FightingLane nextLane;
        
        public List<BadWord> BadWords { get; private set; } = new();
        public List<PreciousWord> PreciousWords { get; private set; } = new();

        private PreciousWord lastPreciousWord;
        private bool _canSpawn;

        private void Awake()
        {
            _canSpawn = true;
        }

        ///fonction à appeler au moment du drag and drop (faire passer word data dans fightingData et null dans parent
        public FightingWord Spawn(IFightingData fightingData, Transform parent)
        {
            if (!CanSpawn)
                return null;
            
            bool ally = fightingData is WordData;
            Transform position = ally ? allyPosition : enemyPosition;
            return Spawn(fightingData, parent, position.position);
        }

        public FightingWord Spawn(IFightingData fightingData, Transform parent, Vector2 position)
        {
            if (!CanSpawn)
                return null;
            
            FightingWord word = Instantiate(fightingData.Prefab, position, Quaternion.identity, parent ? parent : transform);
            word.Init(fightingData, this);

            if (word is BadWord badWord)
                BadWords.Add(badWord);
            else if(word is PreciousWord preciousWord)
            {
                lastPreciousWord = preciousWord;
                PreciousWords.Add(preciousWord);
            }
            
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

        public void RemoveAbilityToSpawn()
        {
            _canSpawn = false;
        }

        public void ResetAbilityToSpawn()
        {
            _canSpawn = true;
        }
    }
}
