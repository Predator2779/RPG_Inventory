using UnityEngine;
using UnityEngine.EventSystems;

namespace Inventory.DragNDrop
{
    public class Dragedable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
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
            SlotView outputSlot = eventData.pointerEnter != null ? eventData.pointerEnter.GetComponentInParent<SlotView>() : null;
            
            if (outputSlot != null) outputSlot.DragFrom(inputSlot.Index);

            transform.SetParent(_originalParent);
            transform.localPosition = Vector3.zero;
        }
    }
}