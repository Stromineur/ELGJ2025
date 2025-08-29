using System;
using System.Collections.Generic;
using LucidFactory.Cards.UI.Internal;
using LucidFactory.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace LucidFactory.Cards.UI
{
    /// <summary>
    /// Gère l'affichage des cartes
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class CardUI<T> : CardUI<T, CardUI<T>, IDropSlot>
        where T : ICard
    {
        protected sealed override CardUI<T> GetCardUITarget() => this;
    }
    
    
    namespace Internal
    {
        /// <summary>
        /// DONT INHERIT FROM THIS CLASS
        /// </summary>
        /// <typeparam name="T">Card</typeparam>
        /// <typeparam name="TU">CardUI</typeparam>
        /// <typeparam name="TD">DropSlot</typeparam>
        public abstract class CardUI<T, TU, TD> : Draggable<TD>,
            ICardUI<T>,
            ICardUIInitializer<T>,

        IPointerEnterHandler,
        IPointerExitHandler

        where T : ICard
        where TU : ICardUI
        where TD : IDropSlot
        {

        public event Action<TU> OnPointerEntered;
        public event Action<TU> OnPointerExited;
        public event Action<TU> OnDragBegun;
        public event Action<TU, DropContext> OnDropSucceed;
        public event Action<TU> OnDropCanceled;
        public event Action<TU> OnDragEnded;
        public event Action<TU, Vector2> OnDragged;
        public event Action<TU> OnInit;
        public event Action<TU> OnDeInit;
        protected CardUI<T, TU, TD> placeholderCard { get; private set; }

        protected override IEnumerable<TD> Slots => dropSlotProvider?.GetSlotsForCard(GetCardUITarget());

        private ICardSlotProvider<TU, TD> dropSlotProvider;
        private new Camera camera;
        
        public T Card { get; private set; }




        protected override void Awake()
        {
            base.Awake();
            this.camera = Canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : Canvas.worldCamera;
        }

        #region Initialisation

        public void Init(T card, bool withPlaceholder = false)
        {
            Card = card;

            if (withPlaceholder)
            {
                placeholderCard = InstantiatePlaceHolder();
                placeholderCard.InitAsPlaceHolder(this);
            }

            DoInit(card);
            OnInit?.Invoke(GetCardUITarget());
        }

        protected virtual CardUI<T, TU, TD> InstantiatePlaceHolder()
        {
            return Instantiate(this, transform.position, Quaternion.identity, transform.parent);
        }

        /// <summary>
        /// Called within the card system himself.
        /// This sets this cards has a placeholder, a mirror of an other cardUI.
        /// </summary>
        /// <param name="cardUI">CardUI to mirror</param>
        protected virtual void InitAsPlaceHolder(CardUI<T, TU, TD> cardUI)
        {
            Init(cardUI.Card, false);

            CanBeDragged = false;
            gameObject.SetActive(false);
        }

        public void DeInit(T card)
        {
            OnDeInit?.Invoke(GetCardUITarget());
            DoDeInit(card);
            if (placeholderCard)
            {
                placeholderCard.DeInit(card);
            }
        }

        /// <summary>
        /// Custom init implementation
        /// </summary>
        /// <param name="card"></param>
        protected abstract void DoInit(T card);

        /// <summary>
        /// Custom Deinit implementation
        /// </summary>
        /// <param name="card"></param>
        protected abstract void DoDeInit(T card);

        #endregion

        public void SetAsDraggable(ICardSlotProvider<TU, TD> provider)
        {
            CanBeDragged = true;
            dropSlotProvider = provider;
        }

        protected override void OnDragInit(Vector2 screenPos)
        {
            
        }

        protected override void OnDragBegin()
        {
            // Debug.Log("Beginning drag");
            OnDragBegun?.Invoke(GetCardUITarget());

            if (placeholderCard)
                placeholderCard.gameObject.SetActive(true);
        }

        protected override void OnDragEnd(Vector2 position)
        {
            OnDragEnded?.Invoke(GetCardUITarget());
            
            // Debug.Log("Ending drag");
            if (CurrentSlot == null)
                OnDropCanceled?.Invoke(GetCardUITarget());
            else
                OnDropSucceed?.Invoke(GetCardUITarget(),
                    new DropContext { DropSlot = CurrentSlot, Position = position });
            
            if (placeholderCard)
                placeholderCard.gameObject.SetActive(false);
        }

        protected override void OnDrag(Vector2 screenPos)
        {
            Vector3 worldPoint = camera ? camera.ScreenToWorldPoint(screenPos) : screenPos;
            if (Canvas.renderMode == RenderMode.ScreenSpaceCamera)
            {
                worldPoint += camera.transform.forward * Canvas.planeDistance;
            }

            transform.position = worldPoint;
            OnDragged?.Invoke(GetCardUITarget(), screenPos);
        }

        protected override bool IsDragValid(PointerEventData eventData) => true;

        public virtual void SetParent(Transform parent)
        {
            transform.SetParent(parent);
        }


        protected virtual void OnPointerEnter(PointerEventData eventData)
        {
            OnPointerEntered?.Invoke(GetCardUITarget());
        }

        protected virtual void OnPointerExit(PointerEventData eventData)
        {
            OnPointerExited?.Invoke(GetCardUITarget());
        }


        protected abstract TU GetCardUITarget();

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData) => OnPointerEnter(eventData);
        void IPointerExitHandler.OnPointerExit(PointerEventData eventData) => OnPointerExit(eventData);
        }
    }
}
