using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace LucidFactory.Cards
{
    /// <summary>
    /// Handles a hand of the player
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [System.Serializable]
    public class Hand<T> : CardHolder<T> where T : ICard
    {
        public event Action OnCardOrderChanged;
        
        [ShowInInspector, HideInEditorMode]
        public List<T> CardsList { get; private set; }
        public override IEnumerable<T> Cards => CardsList;
        public override int MaxSize { get; }
        
        public Hand(IEnumerable<T> cards, int maxSize) : base(cards)
        {
            CardsList = new List<T>();
            MaxSize = maxSize;
        }
        
        public Hand(int maxSize) : base()
        {
            CardsList = new List<T>();
            MaxSize = maxSize;
        }

        /// <summary>
        /// Adds a given card to the hand
        /// </summary>
        /// <param name="card">Card to add to the hand</param>
        protected override void AddCard(T card)
        {
            CardsList.Add(card);
        }

        protected override void RemoveCard(T card)
        {
            CardsList.Remove(card);
        }

        public override bool HasCard(T card)
        {
            return CardsList.Contains(card);
        }

        public virtual int GetCardIndex(T card) => CardsList.IndexOf(card);

        public virtual bool SetCardIndex(T card, int index)
        {
            if (HasCard(card) && index >= 0 && index < Size)
            {
                CardsList.Remove(card);
                CardsList.Insert(index, card);
                OnCardOrderChanged?.Invoke();
                return true;
            }

            return false;
        }

        /*public void InsertCard(CardUI card, int index)
        {
            Debug.Log(index);
            Debug.Log(card.transform.parent.GetSiblingIndex());
            int previousIndex = handCards.IndexOf(card);
            Debug.Log(previousIndex);
            if (index == -1)
            {
                handCards.Add(card);
                handCardsData.Add(card.card);
            }
            else
            {
                handCards.Insert(index, card);
                handCardsData.Insert(index, card.card);
            }
            //handCards.RemoveAt(previousIndex);
            //handCardsData.RemoveAt(previousIndex);
        }*/
    }
}
