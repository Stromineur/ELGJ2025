using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NecroMotMicon.Script.Words
{
    public class DragNDropEvents : MonoBehaviour
    {
        public event Action OnWordStartDrag;
        public event Action OnWordDrop;
        
        private List<DragWord> _draggableWord = new();

        private void Awake()
        {
            _draggableWord = GetComponentsInChildren<DragWord>().ToList();
        }

        private void OnEnable()
        {
            foreach (DragWord dragWord in _draggableWord)
            {
                dragWord.OnWordStartDrag += StartDrag;
                dragWord.OnWordDrop += StopDrag;
            }
        }

        private void OnDisable()
        {
            foreach (DragWord dragWord in _draggableWord)
            {
                dragWord.OnWordStartDrag -= StartDrag;
                dragWord.OnWordDrop -= StopDrag;
            }
        }

        private void StartDrag()
        {
            OnWordStartDrag?.Invoke();
        }

        private void StopDrag()
        {
            OnWordDrop?.Invoke();
        }
    }
}
