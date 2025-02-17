using Script.FightingPlan;

namespace Script.Words
{
    public interface IFightingData
    {
        public FightingWord Prefab { get; }
        public float Speed { get; }
    }
}
