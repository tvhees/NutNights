using Components;
using GameData;
using UnityEngine;
using UnityEngine.UI;

namespace Collections
{
    public class CardButton : MonoBehaviour
    {
        public Button button;
        public Image buttonImage;
        public SpriteOutline spriteOutline;

        public void UpdateView(Card card, bool interactible)
        {
            spriteOutline.OutlineColor = card.color;
            var sprite = Resources.Load<Sprite>(card.type.ToString());
            buttonImage.sprite = sprite;
            button.interactable = interactible;
        }

    }
}