using DG.Tweening;
using UnityEngine;

namespace GameData
{
    public static class Animations
    {
        public static void Grow(this RectTransform rect)
        {
            rect.sizeDelta = Vector2.zero;
            rect.DOSizeDelta(new Vector2(100, 160), 2f);
        }
    }
}