using UnityEngine;

namespace Untold.Core.Stories
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Hover/Dictionary")]
    public class HoverDictionaryData : ScriptableObject
    {
        [SerializeField] private string _dictionaryName;
        [SerializeField] private HoverInfo[] _wordsToHover;

        public string DictionaryName => _dictionaryName;
        public HoverInfo[] WordsToHover => _wordsToHover;

        /// <summary>
        /// Compare words to define if the link we are hover is in dictionary
        /// </summary>
        /// <param name="linkText">Word to search in dictionary</param>
        /// <returns>return the ObjectToHover or an empty ObjectToHover if not found</returns>
        public bool SearchInDictionary(string title, out HoverInfo hoverInfo)
        {
            foreach (HoverInfo info in WordsToHover)
            {
                if(info.Title == title)
                {
                    hoverInfo = info;
                    return true;
                }
            }

            hoverInfo = null;
            return false;
        }
    }
}
