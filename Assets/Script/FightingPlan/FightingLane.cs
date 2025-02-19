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
        public event Action OnCanSpawn;
        public event Action OnCantSpawn;
        
        public bool CanSpawnPrecious => _canSpawnPrecious &&
                                        (!lastPreciousWord || lastPreciousWord && lastPreciousWord.IsInitialized);
        public bool CanSpawnBad => _canSpawnBad;
        public FightingLane PreviousLane => previousLane;
        public FightingLane NextLane => nextLane;
        
        [SerializeField] private Transform allyPosition;
        [SerializeField] private Transform enemyPosition;
        [SerializeField] private FightingLane previousLane;
        [SerializeField] private FightingLane nextLane;
        
        public List<BadWord> BadWords { get; private set; } = new();
        public List<PreciousWord> PreciousWords { get; private set; } = new();

        private PreciousWord lastPreciousWord;
        private bool _canSpawnPrecious;
        private bool _canSpawnBad;

        private void Awake()
        {
            _canSpawnPrecious = true;
            _canSpawnBad = true;
            OnCanSpawn?.Invoke();
        }

        ///fonction à appeler au moment du drag and drop (faire passer word data dans fightingData et null dans parent
        public FightingWord Spawn(IFightingData fightingData, Transform parent, bool exhuming = true)
        {
            bool ally = fightingData is WordData;
            Transform position = ally ? allyPosition : enemyPosition;
            return Spawn(fightingData, parent, position.position, exhuming);
        }

        public FightingWord Spawn(IFightingData fightingData, Transform parent, Vector2 position, bool exhuming = true)
        {
            bool ally = fightingData is WordData;
            if (ally && !CanSpawnPrecious)
                return null;
            if (!ally && !CanSpawnBad)
                return null;
            
            FightingWord word = Instantiate(fightingData.Prefab, position, Quaternion.identity, parent ? parent : transform);

            if (word is BadWord badWord)
                BadWords.Add(badWord);
            else if(word is PreciousWord preciousWord)
            {
                lastPreciousWord = preciousWord;
                PreciousWords.Add(preciousWord);
                OnCantSpawn?.Invoke();
                preciousWord.OnInitialized += OnPreciousWordInitialized;
            }
            word.Init(fightingData, this, exhuming);
            
            word.OnDeath += OnWordDeath;
            
            return word;
        }

        private void OnPreciousWordInitialized()
        {
            OnCanSpawn?.Invoke();
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
            _canSpawnPrecious = false;
        }

        public void ResetAbilityToSpawn()
        {
            _canSpawnPrecious = true;
        }

        public void AddWordToLane(FightingWord word)
        {
            if (word is BadWord badWord)
                BadWords.Add(badWord);
            else if(word is PreciousWord preciousWord)
                PreciousWords.Add(preciousWord);
        }

        public void RemoveWordFromLane(FightingWord word)
        {
            if (word is BadWord badWord)
                BadWords.Remove(badWord);
            else if(word is PreciousWord preciousWord)
                PreciousWords.Remove(preciousWord);
        }
    }
}
