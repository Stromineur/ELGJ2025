using System;
using DamageNumbersPro;
using UnityEngine;

namespace NecroMotMicon.Script.FightingPlan.Feedback
{
    public class InkValueDisplay : MonoBehaviour
    {
        [SerializeField] private DamageNumber inkValueMesh;
        private BadWord badWord;

        private void Awake()
        {
            badWord = GetComponent<BadWord>();
        }

        private void OnEnable()
        {
            badWord.OnDeath += DisplayInk;
        }

        private void OnDisable()
        {
            badWord.OnDeath -= DisplayInk;
        }

        private void DisplayInk(FightingWord arg1, FightingWord arg2)
        {
             inkValueMesh.Spawn(transform.position, badWord.BadWordData.InkOnDeath);
        }
    }
}
