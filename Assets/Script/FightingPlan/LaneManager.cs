using System.Collections.Generic;
using System.Linq;
using LTX.Singletons;
using UnityEngine;

namespace Script.FightingPlan
{
    public class LaneManager : MonoSingleton<LaneManager>
    {
        public List<FightingLane> FightingLanes { get; private set; } = new();
    
        protected override void Awake()
        {
            base.Awake();

            FightingLanes = FindObjectsByType<FightingLane>(FindObjectsInactive.Exclude, FindObjectsSortMode.InstanceID).ToList();
        }

        public void AddExhumingTime(float addedTime)
        {
            foreach (FightingLane lane in FightingLanes)
            {
                foreach (PreciousWord preciousWord in lane.PreciousWords)
                {
                    if (!preciousWord.IsInitialized)
                    {
                        preciousWord.AddExhumingTime(addedTime);
                    }
                }
            }
        }
    }
}
