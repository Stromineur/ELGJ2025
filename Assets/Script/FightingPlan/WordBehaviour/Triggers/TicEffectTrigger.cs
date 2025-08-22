using System;
using UnityEngine;

namespace NecroMotMicon.Script.FightingPlan.WordBehaviour.Triggers
{
    public class TicEffectTrigger : EffectTrigger
    {
        [SerializeField] private float tic = 1;
        private string triggerMethodName;
        
        protected override void Setup()
        {
            SubscribeToEvents();
            triggerMethodName = nameof(Trigger);
        }
        
        private void OnEnable()
        {
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            if(isSetup)
                return;
            if(!fightingWord)
                return;
            
            fightingWord.OnInitialized += StartTrigger;
            fightingWord.OnDeath += StopTrigger;
            isSetup = true;
        }

        private void OnDisable()
        {
            if(!fightingWord)
                return;
            
            fightingWord.OnInitialized -= StartTrigger;
            fightingWord.OnDeath -= StopTrigger;
            isSetup = false;
        }

        private void StartTrigger()
        {
            InvokeRepeating(triggerMethodName, tic, tic);
        }

        private void StopTrigger(FightingWord arg1, FightingWord arg2)
        {
            CancelInvoke(triggerMethodName);
        }
    }
}
