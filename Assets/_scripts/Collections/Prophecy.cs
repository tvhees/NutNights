using System.Collections.Generic;
using System.Diagnostics;
using Components;
using Controllers;
using GameData;
using UnityEngine.UI;

namespace Collections
{
    public class Prophecy : Collection {

        protected override void Awake()
        {
            Controller = GetManager<ProphecyController>();
            base.Awake();
        }

        public void AddButtons()
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

        public override Button AddButton(int index)
        {
            throw new System.NotImplementedException();
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
