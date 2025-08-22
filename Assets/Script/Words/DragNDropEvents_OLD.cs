using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NecroMotMicon.Script.Words
{
    public class DragNDropEvents_OLD : MonoBehaviour
    {
        public event Action OnWordDrag;
        public event Action OnWordDrop;
        
        private List<WordTemplate_OLD> _wordTemplates = new();

        private void Awake()
        {
            _wordTemplates = GetComponentsInChildren<WordTemplate_OLD>().ToList();
        }

        private void OnEnable()
        {
            foreach (WordTemplate_OLD wordTemplate in _wordTemplates)
            {
                wordTemplate.OnWordDrag += StartDrag;
                wordTemplate.OnWordDrop += StopDrag;
            }
        }

        private void OnDisable()
        {
            foreach (WordTemplate_OLD wordTemplate in _wordTemplates)
            {
                wordTemplate.OnWordDrag -= StartDrag;
                wordTemplate.OnWordDrop -= StopDrag;
            }
        }

        private void StartDrag()
        {
            OnWordDrag?.Invoke();
        }

        private void StopDrag()
        {
            OnWordDrop?.Invoke();
        }
    }
}
