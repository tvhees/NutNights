using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Controllers;
using GameData;
using UnityEngine;
using UnityEngine.UI;

namespace Collections
{
    public class Discard : Collection
    {
        [SerializeField] private Text discardedCards;

        protected override void Awake()
        {
            Controller = GetManager<DiscardController>();
            base.Awake();
        }

        public override Button AddButton(Button button, int index = 0)
        {
            game.ReturnCardButton(button);
            return button;
        }

        public override void UpdateView(List<Card> cards)
        {
            discardedCards.text = cards.Count.ToString();
        }
    }
}
