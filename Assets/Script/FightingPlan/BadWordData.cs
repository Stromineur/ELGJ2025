using Sirenix.OdinInspector;
using UnityEngine;

namespace NecroMotMicon.Script.FightingPlan
{
    [CreateAssetMenu(fileName = "BadWordData", menuName = "Scriptable Objects/BadWordData")]
    public class BadWordData : ScriptableObject, IFightingData
    {
        public FightingWord Prefab => prefab;
        public float Speed => speed;
        public float Hp => hp;
        public float Damage => damage;
        public float BookDamage => bookDamage;
        public int InkOnDeath => inkOnDeath;

        [SerializeField] private FightingWord prefab;
        [SerializeField, LabelText("Vitesse")] private float speed;
        [SerializeField, LabelText("PV")] private float hp;
        [SerializeField, LabelText("Dégâts")] private float damage;
        [SerializeField, LabelText("Dégâts au livre")] private float bookDamage = 1;
        [SerializeField, LabelText("Encre à la mort")] private int inkOnDeath;
    }
}
