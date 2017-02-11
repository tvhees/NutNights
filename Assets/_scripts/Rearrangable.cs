using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using Collections;

public class Rearrangable : MonoBehaviour, IDropHandler, IBeginDragHandler
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
}
