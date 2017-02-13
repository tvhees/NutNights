using Collections;
using GameStates;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Components
{
    public class Rearrangable : MonoBehaviour, IBeginDragHandler, IDropHandler, IDragHandler, IEndDragHandler
    {
        private Button button;
        private CanvasGroup canvasGroup;
        private Vector2 startPosition;

        public void OnEnable()
        {
            button = GetComponent<Button>();
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void OnDrop(PointerEventData eventData)
        {
            var i = eventData.pointerDrag.transform.GetSiblingIndex();
            var j = transform.GetSiblingIndex();
            var collection = GetComponentInParent<Collection>();
            collection.SwapCards(i, j);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            button.interactable = false;
            canvasGroup.blocksRaycasts = false;
            startPosition = transform.position;
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            button.interactable = true;
            canvasGroup.blocksRaycasts = true;
            transform.position = startPosition;
        }
    }
}
