using Sirenix.OdinInspector;
using UnityEngine;

namespace NecroMotMicon.Script.FightingPlan.WordBehaviour.Triggers
{
    public class HpThresholdEffectTrigger : EffectTrigger
    {
        [SerializeField, PropertyRange(0, 100), LabelText("% vie")] private int pourcentThreshold;
        private float threshold;
        private bool _triggered;

        private void Awake()
        {
            threshold = (float) pourcentThreshold / 100;
        }

        protected override void Setup()
        {
            SubscribeToEvents();
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
            
            fightingWord.OnHpChanged += CheckHp;
            isSetup = true;
        }

        private void OnDisable()
        {
            if(!fightingWord)
                return;
            
            fightingWord.OnHpChanged -= CheckHp;
            isSetup = false;
        }

        private void CheckHp(float currentHp, float maxHp)
        {
            if(_triggered)
                return;
            
            if (threshold >= currentHp / maxHp)
            {
                Trigger();
                _triggered = true;
            } 
        }
    }
}
