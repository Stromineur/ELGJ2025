using NecroMotMicon.Script.FightingPlan;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NecroMotMicon.Script.Words
{
    [CreateAssetMenu(fileName = "WordData", menuName = "Scriptable Objects/WordData")]
    public class WordData : ScriptableObject, IFightingData
    {
        public GameObject wordPrefab;
        
        public string wordName;
        public string wordDescription;
        public string wordEffect;
        
        public float writingTime;
        public int writingCost;
        public int exhumingCost;

        #region Fighting

        public float ExhumingTime => exhumingTime;
        public float Speed => speed;
        public float Hp => hp;
        public float BaseDamage => baseDamage;
        public BadWordData[] StrongAgainst => strongAgainst;
        public FightingWord Prefab => prefab;
        
        [SerializeField, FoldoutGroup("InGame"), LabelText("Temps d'exhumation")] private float exhumingTime;
        [SerializeField, FoldoutGroup("InGame"), LabelText("Vitesse")] private float speed;
        [SerializeField, LabelText("PV")] private float hp;
        [SerializeField, FoldoutGroup("InGame"), LabelText("Dégâts")] private float baseDamage;
        [SerializeField, FoldoutGroup("InGame"), LabelText("Prévalence")] private BadWordData[] strongAgainst;
        [SerializeField, FoldoutGroup("InGame"), LabelText("Prefab")] private FightingWord prefab;

        #endregion
    }
}
