﻿using UnityEngine;
using UnityEngine.EventSystems;

namespace Inventory
{
    public class DragNDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private Transform _originalParent;
        private Canvas _canvas;
        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _canvas = GetComponentInParent<Canvas>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _originalParent = transform.parent;
            transform.SetParent(_canvas.transform);
            _canvasGroup.blocksRaycasts = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _canvas.transform as RectTransform,
                eventData.position,
                _canvas.worldCamera,
                out var localPoint
            );
            transform.localPosition = localPoint;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _canvasGroup.blocksRaycasts = true;

            SlotView inputSlot = _originalParent.GetComponent<SlotView>();
            SlotView outputSlot = eventData.pointerEnter != null ? eventData.pointerEnter.GetComponent<SlotView>() : null;

            print(outputSlot);
            
            if (outputSlot != null) outputSlot.onItemChanged?.Invoke(inputSlot, outputSlot);

            // if (newSlotView != null && newSlotView.GetItem() == null)
            // {
            //     newSlotView.SetItem(originalSlotView.GetItem());
            //     originalSlotView.ClearSlot();
            // }

            transform.SetParent(_originalParent);
            transform.localPosition = Vector3.zero;
        }
    }
}