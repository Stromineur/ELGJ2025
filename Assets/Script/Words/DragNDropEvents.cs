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
        
        private List<BookWord> _draggableWord = new();

        private void Awake()
        {
            _draggableWord = GetComponentsInChildren<BookWord>().ToList();
        }

        private void OnEnable()
        {
            foreach (BookWord dragWord in _draggableWord)
            {
                dragWord.OnWordStartDrag += StartDrag;
                dragWord.OnWordDrop += StopDrag;
            }
        }

        private void OnDisable()
        {
            foreach (BookWord dragWord in _draggableWord)
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
