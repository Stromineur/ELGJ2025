using System;
using System.Collections.Generic;
using System.Linq;
using NecroMotMicon.Script.FightingPlan;
using NecroMotMicon.Script.FightingPlan.Wave;
using NecroMotMicon.Script.Words;
using UnityEngine;

namespace NecroMotMicon
{
    public class InkGain : MonoBehaviour
    {
        private GameObject fightingLaneContainer;
        private WordManager _wordManager;
        private WaveManager _waveManager;
        private List<FightingLane> _fightingLanes = new();
        
        private void Awake()
        {
            fightingLaneContainer = GameObject.Find("FightingLanes");
            _fightingLanes = fightingLaneContainer.GetComponentsInChildren<FightingLane>().ToList();
            _wordManager = GetComponentInChildren<WordManager>();
            _waveManager = GameObject.Find("WaveManager").GetComponent<WaveManager>();
        }

        private void OnEnable()
        {
            foreach (FightingLane fightingLane in _fightingLanes)
            {
                fightingLane.InkOnDeath += InkOnDeath;
            }
            _waveManager.OnWaveEnd += InkOnWaveEnd;
        }

        private void OnDisable()
        {
            foreach (FightingLane fightingLane in _fightingLanes)
            {
                fightingLane.InkOnDeath -= InkOnDeath;
            }
            _waveManager.OnWaveEnd -= InkOnWaveEnd;
        }
        
        private void InkOnDeath(BadWord killedWord)
        {
            _wordManager.UpdateTotalInk(killedWord.BadWordData.InkOnDeath, true);
        }

        private void InkOnWaveEnd(WaveData waveData)
        {
            _wordManager.UpdateTotalInk(waveData.InkOnEndWave, true);
        }
    }
}
