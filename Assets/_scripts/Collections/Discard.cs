using System.Collections.Generic;
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

        public override Button AddButton(int index)
        {
            throw new System.NotImplementedException();
        }

        public override void UpdateView(List<Card> cards)
        {
            discardedCards.text = cards.Count.ToString();
        }
    }
}
