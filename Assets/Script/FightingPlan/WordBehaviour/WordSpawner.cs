using System;
using NecroMotMicon.Script.FightingPlan.Wave;
using NecroMotMicon.Script.Words;
using Script.Core;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace NecroMotMicon.Script.FightingPlan.WordBehaviour
{
    public class WordSpawner : WordBehaviour
    {
        [SerializeField] private bool preciousWord;
        [SerializeField, HideIf(nameof(preciousWord))] private BadWordData badWordToSpawn;
        [SerializeField, ShowIf(nameof(preciousWord))] private WordData preciousWordToSpawn;
        [SerializeField] private SpawnPosition spawnPosition;
        [SerializeField] private float relativeX;
        [SerializeField] private LanePosition lanePosition;
        [SerializeField] private float odds = 100;
        [SerializeField] private float delay;
        [SerializeField] private bool shouldDestroy;

        private Vector2 spawnPos;
        
        protected override void Awake()
        {
            base.Awake();
            
            transform.SetParent(null);
        }

        private void HandleDelay()
        {
            if (spawnPosition == SpawnPosition.CurrentPos && fightingWord != null)
            {
                spawnPos = fightingWord.transform.position - new Vector3(relativeX, 0);
            }
            
            if (odds >= Random.Range(0, 100))
            {
                Invoke(nameof(SpawnWord), delay);
                return;
            }
            
            if(shouldDestroy)
                Destroy(gameObject);
        }

        private void SpawnWord()
        {
            FightingLane lane = lanePosition switch
            {
                LanePosition.Current => fightingWord.FightingLane,
                LanePosition.Top => fightingWord.FightingLane.TopLane,
                LanePosition.Bottom => fightingWord.FightingLane.BottomLane,
                _ => fightingWord.FightingLane
            };
            
            if(lane != null)
            {
                if (!preciousWord)
                {
                    FightingWord word = spawnPosition == SpawnPosition.Spawn ? 
                        ServiceLocator.Instance.WaveManager.SpawnEnemy(badWordToSpawn, null, lane) :
                        ServiceLocator.Instance.WaveManager.SpawnEnemy(badWordToSpawn, null, lane, spawnPos);
                    if(word is BadWord badWord)
                        ServiceLocator.Instance.WaveManager.CurrentWave.AddBadWord(badWord);
                }
                else if (fightingWord is PreciousWord)
                {
                    if (spawnPosition == SpawnPosition.Spawn)
                        lane.Spawn(preciousWordToSpawn, null, false);
                    else
                        lane.Spawn(preciousWordToSpawn, null, spawnPos, false);
                }
            }
            
            if(shouldDestroy)
                Destroy(gameObject);
        }

        public override void Trigger()
        {
            HandleDelay();
        }
    }

    public enum SpawnRequirement
    {
        None,
        OnDeath,
        OnSpawn,
        OnHit
    }

    public enum SpawnPosition
    {
        None,
        Spawn,
        CurrentPos
    }

    public enum LanePosition
    {
        Current,
        Top,
        Bottom,
        All
    }
}
