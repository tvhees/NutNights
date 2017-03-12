using System.Collections.Generic;
using Components;
using Controllers;
using GameData;
using UnityEngine.UI;

namespace Collections
{
    public class Prophecy : Collection {

        public Path HoldPath;

        protected override void Awake()
        {
            Controller = GetManager<ProphecyController>();
            base.Awake();
        }

        public override Button AddButton(Button button, int index = 0)
        {
            button.transform.SetSiblingIndex(index);
            button.onClick.AddListener(() => GetManager<GameController>().OnProphecyCardPressed(index));
            button.gameObject.AddComponent<Rearrangable>();
            button.rectTransform().localPosition = HoldPath.GetPoint(index);
            button.rectTransform().Grow();
            return button;
        }

        public override void UpdateView(List<Card> cards)
        {
            var buttons = GetComponentsInChildren<CardButton>(true);
            var isInteractible = GetManager<StateController>().IsInProphecy;
            for (var i = 0; i < cards.Count; i++)
            {
                buttons[i].UpdateView(cards[i], isInteractible);
                buttons[i].gameObject.SetActive(true);
            }

            for (var i = cards.Count; i < buttons.Length; i++)
            {
                buttons[i].gameObject.SetActive(false);
            }
        }

    }
}
