using UnityEngine;

namespace NecroMotMicon.Script.FightingPlan.WordBehaviour.Feel
{
    public class DeathFXController : MonoBehaviour
    {
        [SerializeField] private ParticleSystem particleSystem;

        private FightingWord preciousWord;
        
        private void Awake()
        {
            preciousWord = GetComponentInParent<FightingWord>();
        }

        private void OnEnable()
        {
            preciousWord.OnDeath += PlayFX;
        }

        private void OnDisable()
        {
            preciousWord.OnDeath -= PlayFX;
        }

        private void PlayFX(FightingWord fightingWord, FightingWord word)
        {
            transform.parent = preciousWord.transform.parent;
            
            particleSystem.Play();
        }
    }
}
