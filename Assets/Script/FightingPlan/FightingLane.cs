using System;
using System.Collections.Generic;
using NecroMotMicon.Script.Words;
using Script.Core;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace NecroMotMicon.Script.FightingPlan
{
    public class FightingLane : MonoBehaviour
    {
        public event Action OnCanSpawn;
        public event Action OnCantSpawn;

        public event Action<BadWord> InkOnDeath;
        
        public event Action OnSpawnPreciousWord;
        
        public bool CanSpawnPrecious => _canSpawnPrecious &&
                                        (!lastPreciousWord || lastPreciousWord && lastPreciousWord.IsInitialized);
        public bool CanSpawnBad => _canSpawnBad;
        public Transform AllyPosition => allyPosition;
        public Transform EnemyPosition => enemyPosition;
        public FightingLane TopLane => topLane;
        public FightingLane BottomLane => bottomLane;
        
        [SerializeField] private Transform allyPosition;
        [SerializeField] private Transform enemyPosition;
        [SerializeField] private FightingLane topLane;
        [SerializeField] private FightingLane bottomLane;
        [SerializeField] private Image exhumingBar;
        
        public List<FightingWord> FightingWords { get; private set; } = new();
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
            
            if (exhuming)
            {
                if (ally && !CanSpawnPrecious)
                    return null;
                if (!ally && !CanSpawnBad)
                    return null;
            }

            position = new Vector3(position.x, Mathf.Clamp(position.y, allyPosition.position.y, enemyPosition.position.y));
            Vector3 spawnPosition = new Vector3(position.x, ally ? allyPosition.position.y : enemyPosition.position.y);
            
            FightingWord word = Instantiate(fightingData.Prefab, spawnPosition, Quaternion.identity, parent ? parent : transform);

            if (word is BadWord badWord)
                AddBadWordToList(badWord);
            else if(word is PreciousWord preciousWord)
            {
                OnSpawnPreciousWord?.Invoke();
                lastPreciousWord = preciousWord;
                AddPreciousWordToList(preciousWord);
                OnCantSpawn?.Invoke();
                preciousWord.OnInitialized += OnPreciousWordInitialized;
            }
            word.Init(fightingData, this, exhuming);
            
            SubscribeToDeathEvent(word);
            
            return word;
        }

        private void SubscribeToDeathEvent(FightingWord word)
        {
            word.OnDeath += OnWordDeath;
        }

        private void UnsubscribeToDeathEvent(FightingWord word)
        {
            word.OnDeath -= OnWordDeath;
        }

        private void OnPreciousWordInitialized()
        {
            OnCanSpawn?.Invoke();
        }

        private void OnWordDeath(FightingWord killed, FightingWord _)
        {
            Debug.Log(killed);
            UnsubscribeToDeathEvent(killed);
            if (killed is BadWord badWord)
            {
                RemoveBadWordFromList(badWord);
                InkOnDeath?.Invoke(badWord);
            }
            else if(killed is PreciousWord preciousWord)
                RemovePreciousWordFromList(preciousWord);
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
            if (!topLane && bottomLane)
                return bottomLane;
            if (!bottomLane && topLane)
                return topLane;
            if (topLane && bottomLane)
            {
                int rnd = Random.Range(0, 2);

                return rnd == 0 ? bottomLane : topLane;
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
                AddBadWordToList(badWord);
            else if(word is PreciousWord preciousWord)
                AddPreciousWordToList(preciousWord);
            SubscribeToDeathEvent(word);
        }

        public void RemoveWordFromLane(FightingWord word)
        {
            if (word is BadWord badWord)
                RemoveBadWordFromList(badWord);
            else if(word is PreciousWord preciousWord)
                RemovePreciousWordFromList(preciousWord);
            UnsubscribeToDeathEvent(word);
        }

        private void AddBadWordToList(BadWord badWord)
        {
            BadWords.Add(badWord);
            FightingWords.Add(badWord);
        }

        private void AddPreciousWordToList(PreciousWord preciousWord)
        {
            PreciousWords.Add(preciousWord);
            FightingWords.Add(preciousWord);
        }

        private void RemoveBadWordFromList(BadWord badWord)
        {
            BadWords.Remove(badWord);
            FightingWords.Remove(badWord);
        }

        private void RemovePreciousWordFromList(PreciousWord preciousWord)
        {
            PreciousWords.Remove(preciousWord);
            FightingWords.Remove(preciousWord);
        }

        public void UpdateExhumingBar(float currentValue, float maxValue)
        {
            exhumingBar.fillAmount = currentValue / maxValue;
        }

        public float GetEnemySpawnOdds()
        {
            if(BadWords.Count == 0)
                return 1;
            if(BadWords.Count >= GameController.GameMetrics.SpawnOdds.Length)
                return GameController.GameMetrics.SpawnOdds[^1];
            return GameController.GameMetrics.SpawnOdds[BadWords.Count];
        }
    }
}
