using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LucidFactory.UI
{
    public abstract class Draggable : MonoBehaviour, IDraggable
    {
        public IDragHandler Fallback { get; set; }
        public bool CanBeDragged { get; set; }
        public bool IsDragged { get; private set; }

        public Canvas Canvas { get; private set; }
        public RectTransform RectTransform { get; private set; }
        
        IDragHandler IDraggable.Fallback => Fallback;

        private bool isDestroy = false;

        protected virtual void Awake()
        {
            isDestroy = false;
            
            Canvas = transform.GetComponentInParent<Canvas>();
            RectTransform = GetComponent<RectTransform>();
        }
        protected virtual void OnDestroy()
        {
            isDestroy = true;
        }
        
        void IInitializePotentialDragHandler.OnInitializePotentialDrag(PointerEventData eventData)
        {
            // Debug.Log("ici?");
            TryInitDrag(eventData);
        }
        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            // Debug.Log("la");
            TryDrag(eventData);
        }

        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {
            // Debug.Log("peut etre ici");
            if (TryBeginDrag(eventData))
                IsDragged = true;
        }

        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            if(TryEndDrag(eventData))
                IsDragged = false;
        }

        protected virtual bool TryInitDrag(PointerEventData eventData)
        {
            if (Fallback is IInitializePotentialDragHandler potential)
            {
                 potential.OnInitializePotentialDrag(eventData);
            }

            OnDragInit(eventData.position);
            return true;
        }
        
        protected virtual bool TryBeginDrag(PointerEventData eventData)
        {
            if (!CanBeDragged)
                return false;

            if (!IsDragValid(eventData))
            {
                if (Fallback != null)
                {
                    if (Fallback is IBeginDragHandler begin)
                        begin.OnBeginDrag(eventData);

                    if (Fallback is Component component)
                        eventData.pointerDrag = component.gameObject;
                }
                else
                    eventData.pointerDrag = null;

                return false;
            }

            OnDragBegin();
            return true;
        }

        protected virtual bool TryDrag(PointerEventData eventData)
        {
            //Debug.Log("1");
            if (!CanBeDragged)
                return false;

            //Debug.Log("2");
            if (!IsDragValid(eventData))
            {
                if (Fallback == null)
                {
                    eventData.pointerDrag = null;
                }
                else
                {
                    Fallback?.OnDrag(eventData);
                    if (Fallback is Component component)
                    {
                        eventData.pointerDrag = component.gameObject;
                    }
                }

                return false;
            }

            //Debug.Log("3");
            OnDrag(eventData.position);
            return true;
        }
        protected virtual bool TryEndDrag(PointerEventData eventData)
        {
            if (!CanBeDragged)
                return false;

            if (!IsDragValid(eventData))
            {
                if (Fallback == null)
                {
                    eventData.pointerDrag = null;
                }
                else
                {
                    if (Fallback is IEndDragHandler end)
                        end.OnEndDrag(eventData);
                }

                return false;
            }
            //Debug.Log("end drag");

            OnDragEnd(eventData.position);
            return true;
        }

        protected abstract void OnDragInit(Vector2 screenPos);
        protected abstract void OnDragBegin();
        protected abstract void OnDragEnd(Vector2 position);
        protected abstract void OnDrag(Vector2 screenPos);
        protected abstract bool IsDragValid(PointerEventData eventData);
        
        void ICanvasElement.Rebuild(CanvasUpdate executing)
        {
            
        }

        void ICanvasElement.LayoutComplete()
        {
            
        }

        void ICanvasElement.GraphicUpdateComplete()
        {
            
        }

        bool ICanvasElement.IsDestroyed()
        {
            return isDestroy;
        }

    }

    public abstract class Draggable<T> : Draggable, IDraggable<T> where T : IDropSlot
    {
        public T CurrentSlot { get; private set; }
        protected abstract IEnumerable<T> Slots { get; }

        T IDraggable<T>.CurrentSlot => CurrentSlot;
        IEnumerable<T> IDraggable<T>.Slots => Slots;


        protected override bool TryDrag(PointerEventData eventData)
        {
            //Debug.Log("Par la");

            if (!base.TryDrag(eventData))
                return false;

            //Debug.Log("La");

            //If still over current slot, no need to find an other
            if (CurrentSlot != null && CurrentSlot.IsDraggableOver(this))
                return true;

            //Debug.Log("Ici");
            //Looking for other slots
            var currentSlotWithCallbacks = CurrentSlot as IDropSlotWithCallbacks;
            
            T selectectSlot = default;

            foreach (T slot in Slots)
            {
                if (slot.IsDraggableOver(this))
                {
                    if(selectectSlot == null)
                        selectectSlot = slot;
                    else if(selectectSlot.Priority < slot.Priority)
                    {
                        selectectSlot = slot;
                    }
                    selectectSlot = selectectSlot != null ? 
                        selectectSlot.Priority < slot.Priority ? 
                            slot : 
                            selectectSlot 
                        : slot;
                }
            }

            bool isOverSlot = selectectSlot != null;
            if (isOverSlot)
            {
                var dropSlotWithCallbacks = selectectSlot as IDropSlotWithCallbacks;
                if (selectectSlot.Equals(CurrentSlot))
                {
                    dropSlotWithCallbacks?.OnDraggableEnter(this);
                    currentSlotWithCallbacks?.OnDraggableExit(this);
                }

                CurrentSlot = selectectSlot;
            }

            if (!isOverSlot && CurrentSlot != null)
            {
                currentSlotWithCallbacks?.OnDraggableExit(this);
                CurrentSlot = default;
            }

            return true;
        }

        protected override bool TryEndDrag(PointerEventData eventData)
        {
            if (!base.TryEndDrag(eventData))
                return false;
            
            if(CurrentSlot is IDropSlotWithCallbacks dropSlotWithCallbacks)
                dropSlotWithCallbacks.OnDraggableDrop(this);
            return true;
        }
    }
}
