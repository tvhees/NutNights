using Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace GameData
{
    public static class ButtonExtensions
    {
        public static void MoveToNewCollection(this Button button, ICollectionObject target)
        {
            target.AddButton(button);
        }

        public static Button Grow(this Button button)
        {
            button.rectTransform().Grow();
            return button;
        }

        public static Button LocalMoveTo(this Button button, Vector3 endValue, float duration = 1.0f)
        {
            button.transform.DOLocalMove(endValue, duration);
            return button;
        }

        public static Button Rotate(this Button button, Vector3 endValue, float duration = 1.0f)
        {
            button.transform.DORotate(endValue, duration, RotateMode.FastBeyond360);
            return button;
        }
    }
}