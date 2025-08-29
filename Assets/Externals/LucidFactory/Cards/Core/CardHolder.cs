using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;

namespace LucidFactory.Cards
{
    [System.Serializable]
    public abstract class CardHolder<T> where T : ICard
    {
        public event Action OnCleared;
        public event Action<T> OnCardAdded;
        public event Action<T> OnCardRemoved;
        
        public abstract IEnumerable<T> Cards { get; }
        
        [ShowInInspector, HideInEditorMode]
        public abstract int MaxSize { get; }
        [ShowInInspector, HideInEditorMode]
        public virtual int Size => Cards.Count();
        [ShowInInspector, HideInEditorMode]
        public bool IsEmpty => Size == 0;
        [ShowInInspector, HideInEditorMode]
        public bool IsFull => Size == MaxSize;

        public CardHolder(IEnumerable<T> cards) : this()
        {
            foreach (T card in cards)
            {
                TryAddCard(card);
            }
        }

        public CardHolder() { }

        /// <summary>
        /// Adds a given card to the hand
        /// </summary>
        /// <param name="card">Card to add to the hand</param>
        protected abstract void AddCard(T card);
        protected abstract void RemoveCard(T card);
        public abstract bool HasCard(T card);

        public bool TryAddCard(T card)
        {
            if (CanAddCard(card))
            {
                AddCard(card);
                OnCardAdded?.Invoke(card);
                return true;
            }

            return false;
        }

        protected virtual bool CanAddCard(T card)
        {
            return MaxSize < 0 || !IsFull;
        }

        public bool TryRemoveCard(T card)
        {
            if (CanRemoveCard(card))
            {
                RemoveCard(card);
                OnCardRemoved?.Invoke(card);
                return true;
            }

            return false;
        }

        protected virtual bool CanRemoveCard(T card)
        {
            return HasCard(card);
        }

        protected virtual void Clear()
        {
            var copy = new List<T>(Cards);
            foreach (var card in copy)
                TryRemoveCard(card);
        }

        public void TryClear()
        {
            Clear();
            OnCleared?.Invoke();
        }
    }
}
