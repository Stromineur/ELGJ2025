using Script.FightingPlan;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Script.Words
{
    public class WordData : ScriptableObject, IFightingData
    {
        public GameObject wordPrefab;
        public Sprite wordSprite;
        
        public string wordName;
        public string wordDescription;
        public string wordEffect;
        
        public float writingTime;

        #region Fighting

        public float ExhumingTime => exhumingTime;
        public float Speed => speed;
        public float BaseDamage => baseDamage;
        public string[] StrongAgainst => strongAgainst;
        public float BoostedDamage => boostedDamage;
        public FightingWord Prefab => prefab;
        
        [SerializeField, FoldoutGroup("InGame"), LabelText("Temps d'exhumation")] private float exhumingTime;
        [SerializeField, FoldoutGroup("InGame"), LabelText("Vitesse")] private float speed;
        [SerializeField, FoldoutGroup("InGame"), LabelText("Dégâts")] private float baseDamage;
        [SerializeField, FoldoutGroup("InGame"), LabelText("Prévalence")] private string[] strongAgainst;
        [SerializeField, FoldoutGroup("InGame"), LabelText("Dégâts de prévalence")] private float boostedDamage;
        [SerializeField, FoldoutGroup("InGame"), LabelText("Prefab")] private FightingWord prefab;

        #endregion
    }
}
