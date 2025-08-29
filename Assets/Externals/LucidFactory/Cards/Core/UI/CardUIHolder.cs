using System;
using LTX.ChanneledProperties;
using LTX.ChanneledProperties.Priorities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LucidFactory.Cards
{
    public abstract class CardUIHolder<T> : CardUIHolder<T, CardHolder<T>>
        where T : ICard
    {

    }

    public abstract class CardUIHolder<T, TU> : MonoBehaviour
        where T : ICard 
        where TU : CardHolder<T>
    {
        public event Action<T> OnCardWasAdded;  
        public event Action<T> OnCardWasRemoved;  
        
        public TU CurrentCardHolder { get; internal set; }
        
        protected CanvasGroup canvasGroup;

        [BoxGroup("Canvas group properties")] 
        public Priority<float> alpha;
        [BoxGroup("Canvas group properties")] 
        public Priority<bool> interactable;

        private bool hasCanvasGroup;

        protected abstract void OnBind(TU cardHandler);
        protected abstract void OnUnbind(TU cardHandler);

        protected abstract void OnCardAdded(T card);

        protected abstract void OnCardRemoved(T card);

        protected virtual void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            hasCanvasGroup = canvasGroup != null;
            
            alpha = new Priority<float>(1);
            alpha.AddOnValueChangeCallback(UpdateCanvasGroupAlpha, true);
            
            interactable = new Priority<bool>(true);
            interactable.AddOnValueChangeCallback(UpdateCanvasGroupInteractable, true);
        }
        
        public void Bind(TU cardHolder)
        {
            if (CurrentCardHolder != null)
            {
                Debug.LogWarning($"[Card Handler UI] Already binded to a cardHandler. Please unbind it first.");
                return;
            }
            
            CurrentCardHolder = cardHolder;
            CurrentCardHolder.OnCardAdded += InternalOnCardAdded;
            CurrentCardHolder.OnCardRemoved += InternalOnCardRemoved;

            OnBind(cardHolder);
        }

        public void UnBind(TU cardHolder)
        {
            if(CurrentCardHolder == null || CurrentCardHolder != cardHolder) return;
            
            CurrentCardHolder.OnCardAdded -= InternalOnCardAdded;
            CurrentCardHolder.OnCardRemoved -= InternalOnCardRemoved;
            CurrentCardHolder = null;
            
            OnUnbind(cardHolder);
        }


        internal void InternalOnCardAdded(T card)
        { 
            OnCardAdded(card);
            OnCardWasAdded?.Invoke(card);
        }
        internal void InternalOnCardRemoved(T card)
        { 
            OnCardRemoved(card);
            OnCardWasRemoved?.Invoke(card);
        }
        
        private void UpdateCanvasGroupAlpha(float alpha)
        {
            if(hasCanvasGroup)
                canvasGroup.alpha = alpha;
        }

        private void UpdateCanvasGroupInteractable(bool interactable)
        {
            if (hasCanvasGroup)
            {
                canvasGroup.interactable = interactable;
                canvasGroup.blocksRaycasts = interactable;
            }
        }
    }
}
