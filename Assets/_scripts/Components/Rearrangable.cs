using Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Components
{
    public class Rearrangable : MonoBehaviour, IDropHandler, IBeginDragHandler, IDragHandler
    {
        private int startIndex;
        private int finishIndex;

        public void OnBeginDrag(PointerEventData eventData)
        {
            startIndex = transform.GetSiblingIndex();
        }

        public void OnDrop(PointerEventData eventData)
        {
            var i = eventData.pointerDrag.transform.GetSiblingIndex();
            var j = transform.GetSiblingIndex();
            var collection = GetComponentInParent<Collection>();
            collection.SwapCards(i, j);
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = eventData.position;
        }
    }
}
