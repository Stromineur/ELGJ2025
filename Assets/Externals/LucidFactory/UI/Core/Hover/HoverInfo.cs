using System;
using UnityEngine;
using UnityEngine.UI;

namespace Untold.Core.Stories
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Hover/Info")]
    public class HoverInfo : ScriptableObject
    {
        [SerializeField] private string _title;
        [SerializeField] private string _description;
        [SerializeField] private Sprite _sprite;

        public string Title => _title;
        public string Description => _description;
        public Sprite Sprite => _sprite;
    }
}