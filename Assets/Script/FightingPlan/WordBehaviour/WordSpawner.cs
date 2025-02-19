using System;
using Script.Core;
using Script.Words;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Script.FightingPlan.WordBehaviour
{
    public class WordSpawner : MonoBehaviour
    {
        [SerializeField] private bool preciousWord;
        [SerializeField, HideIf(nameof(preciousWord))] private BadWordData badWordToSpawn;
        [SerializeField, ShowIf(nameof(preciousWord))] private WordData preciousWordToSpawn;
        [SerializeField] private SpawnRequirement spawnRequirement;
        [SerializeField] private SpawnPosition spawnPosition;
        [SerializeField] private float odds = 100;
        [SerializeField] private float delay;

        private FightingWord _fightingWord;
        private Vector2 spawnPos;

        private void Awake()
        {
            _fightingWord = GetComponentInParent<FightingWord>();
            transform.SetParent(null);
        }

        private void OnEnable()
        {
            switch (spawnRequirement)
            {
                case SpawnRequirement.None:
                    break;
                case SpawnRequirement.OnDeath:
                    _fightingWord.OnDeath += OnDeath;
                    break;
                case SpawnRequirement.OnSpawn:
                    _fightingWord.OnSpawn += OnSpawn;
                    break;
                case SpawnRequirement.OnHit:
                    _fightingWord.OnHit += OnHit;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnDisable()
        {
            switch (spawnRequirement)
            {
                case SpawnRequirement.None:
                    break;
                case SpawnRequirement.OnDeath:
                    _fightingWord.OnDeath -= OnDeath;
                    break;
                case SpawnRequirement.OnSpawn:
                    _fightingWord.OnSpawn -= OnSpawn;
                    break;
                case SpawnRequirement.OnHit:
                    _fightingWord.OnHit -= OnHit;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnDeath(FightingWord arg1, FightingWord arg2)
        {
            if (spawnPosition == SpawnPosition.CurrentPos && _fightingWord != null)
                spawnPos = _fightingWord.transform.position;
            
            HandleDelay();
        }

        private void OnSpawn()
        {
            HandleDelay();
        }

        private void OnHit(float f, FightingWord word)
        {
            if (_fightingWord is BadWord badWord)
            {
                if (badWord.Hp >= f)
                {
                    HandleDelay();
                }
            }
        }

        private void HandleDelay()
        {
            if (odds >= Random.Range(0, 100))
            {
                Invoke(nameof(SpawnWord), delay);
                return;
            }
            
            Destroy(gameObject);
        }

        private void SpawnWord()
        {
            if (spawnPosition == SpawnPosition.CurrentPos && _fightingWord != null)
            {
                spawnPos = _fightingWord.transform.position;
            }
            
            if (!preciousWord)
            {
                if(spawnPosition == SpawnPosition.Spawn)
                    ServiceLocator.Instance.WaveManager.SpawnEnemy(badWordToSpawn, null, _fightingWord.FightingLane);
                else 
                    ServiceLocator.Instance.WaveManager.SpawnEnemy(badWordToSpawn, null, _fightingWord.FightingLane, spawnPos);
            }
            else if(_fightingWord is PreciousWord word)
            {
                if (spawnPosition == SpawnPosition.Spawn)
                    word.FightingLane.Spawn(preciousWordToSpawn, null);
                else 
                    word.FightingLane.Spawn(preciousWordToSpawn, null, spawnPos);
            }
            
            Destroy(gameObject);
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
}
