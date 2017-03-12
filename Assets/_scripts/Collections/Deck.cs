using System.Collections.Generic;
using Controllers;
using GameData;
using UnityEngine;
using UnityEngine.UI;

namespace Collections
{
    public class Deck : Collection
    {
        public int[] doors;
        [SerializeField] private Text remainingCards;

        protected override void Awake()
        {
            Controller = GetManager<DeckController>();
            base.Awake();
        }

        public override Button AddButton(Button button, int index = 0)
        {
            game.ReturnCardButton(button);
            return button;
        }

        public override void UpdateView(List<Card> cards)
        {
            remainingCards.text = cards.Count.ToString();
        }
    }
}
