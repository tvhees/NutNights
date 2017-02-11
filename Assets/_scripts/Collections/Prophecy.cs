using System.Collections.Generic;
using Collections.Controllers;
using UnityEngine.UI;

namespace Collections
{
    public class Prophecy : Collection {

        protected override void Awake()
        {
            Controller = GetManager<ProphecyController>();
            base.Awake();
            AddButtons();
        }

        private void AddButtons()
        {
            if (transform.childCount > 0)
                return;

            for (var i = 0; i < Constants.prophecySize; i++)
            {
                var arg = i;
                var button = game.CreateCardButton(transform);
                button.onClick.AddListener(() => GetManager<GameController>().OnProphecyCardPressed(arg));
                button.gameObject.AddComponent<Rearrangable>();
                button.gameObject.SetActive(false);
            }
        }

        public override void UpdateView(List<Card> cards)
        {
            var images = GetComponentsInChildren<Image>(true);
            for (var i = 0; i < cards.Count; i++)
            {
                images[i].gameObject.SetActive(true);
                images[i].color = cards[i].color;
            }

            for (var i = cards.Count; i < images.Length; i++)
            {
                images[i].gameObject.SetActive(false);
            }
        }

    }
}
