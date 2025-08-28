using UnityEngine;

namespace Script.Core
{
    [CreateAssetMenu(menuName = "Inu/GameMetrics")]
    public class GameMetrics : ScriptableObject
    {
        public static GameMetrics Current
        {
            get
            {
#if UNITY_EDITOR
                if(!Application.isPlaying)
                    return Resources.Load<GameMetrics>("GameMetrics");
#endif
                return GameController.GameMetrics;
            }
        }

        public float ExhumingMultiplier => exhumingMultiplier;
        public float WritingMultiplier => writingMultiplier;
        public float SpeedMultiplier => speedMultiplier;
        public int StartBookHp => startBookHp;
        public float[] SpawnOdds => spawnOdds;
        public int StartInk => startInk;

        [SerializeField] private float exhumingMultiplier = 1;
        [SerializeField] private float writingMultiplier = 1;
        [SerializeField] private float speedMultiplier = 1;
        [SerializeField] private int startBookHp = 10;
        [SerializeField] private float[] spawnOdds;
        [SerializeField] private int startInk = 250;
    }
}