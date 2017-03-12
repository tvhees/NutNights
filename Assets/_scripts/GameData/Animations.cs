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

        public static void AnchorToZero(this RectTransform rect)
        {
            rect.DOAnchorPos(Vector2.zero, 1.0f);
        }
    }
}