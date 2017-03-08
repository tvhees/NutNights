using System.Collections.Generic;
using Controllers;
using GameData;
using UnityEngine;
using System;
using Components;
using DG.Tweening;
using UnityEngine.UI;

namespace Collections
{
    public class Hand : Collection
    {
        public Path HoldPath;

        protected override void Awake()
        {
            Controller = GetManager<HandController>();
            base.Awake();
        }

        public void AddButtons()
        {
            if (transform.childCount > 0)
                return;

            for (var i = 0; i < Constants.handSize; i++)
            {
                AddButton(i);
            }
        }

        public override Button AddButton(int index)
        {
            var button = game.CreateCardButton(transform);
            button.transform.SetSiblingIndex(index);
            button.onClick.AddListener(() => GetManager<GameController>().OnHandCardPressed(index));
            button.rectTransform().localPosition = HoldPath.GetPoint(index);
            button.rectTransform().Grow();
            return button;
        }

        private void UpdateView(List<Card> cards, Func<int, bool> isInteractible) {
            var buttons = GetComponentsInChildren<CardButton>(true);
            for (var i = 0; i < cards.Count; i++)
            {
                buttons[i].UpdateView(cards[i], isInteractible(i));
                buttons[i].gameObject.SetActive(true);
            }

            for (var i = cards.Count; i < buttons.Length; i++)
            {
                buttons[i].gameObject.SetActive(false);
            }
        }

        public override void UpdateView(List<Card> cards)
        {
            UpdateView(cards, (i) => { return IsCardInteractible(cards[i].type); });
        }

        public void UpdateView(List<Card> cards, Color keyColor)
        {
            UpdateView(cards, (i) => { return IsCardInteractible(cards[i], keyColor); });
        }

        private bool IsCardInteractible(Constants.CardType type)
        {
            if (Flags.isDiscarding)
                return true;

            if (Flags.isInNightmare && type == Constants.CardType.Almond)
                return true;

            return !(Flags.isDiscarding || Flags.isInNightmare || Flags.doorDrawn) && type != game.LastType;
        }

        private static bool IsCardInteractible(Card card, Color keyColor)
        {
            return card.type == Constants.CardType.Almond && card.color == keyColor;
        }
    }
}
