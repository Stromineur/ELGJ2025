using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Script.Words
{
    public class DragNDropEvents : MonoBehaviour
    {
        public event Action OnWordDrag;
        public event Action OnWordDrop;
        
        private List<WordTemplate> _wordTemplates = new();

        private void Awake()
        {
            _wordTemplates = GetComponentsInChildren<WordTemplate>().ToList();
        }

        private void OnEnable()
        {
            foreach (WordTemplate wordTemplate in _wordTemplates)
            {
                wordTemplate.OnWordDrag += StartDrag;
                wordTemplate.OnWordDrop += StopDrag;
            }
        }

        private void OnDisable()
        {
            foreach (WordTemplate wordTemplate in _wordTemplates)
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
