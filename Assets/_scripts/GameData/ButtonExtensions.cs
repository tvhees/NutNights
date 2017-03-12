using Collections;
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
    }
}