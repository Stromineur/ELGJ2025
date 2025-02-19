using UnityEngine;

namespace Script.Words
{
    public partial class WordData : ScriptableObject
    {
        public GameObject wordPrefab;
        public Sprite wordSprite;
        
        public string wordName;
        public string wordDescription;
        public string wordEffect;
        
        public float writingTime;
    }
}
