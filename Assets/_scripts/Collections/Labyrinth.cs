using System.Collections.Generic;
using Controllers;
using GameData;
using UnityEngine;

namespace Collections
{
    public class Labyrinth : Collection
    {
        protected override void Awake()
        {
            Controller = GetManager<LabyrinthController>();
            base.Awake();
        }

        public override void UpdateView(List<Card> cards)
        {
            for (var i = transform.childCount; i < cards.Count; i++)
            {
                game.CreateCardButton(transform, cards[i]);
            }
        }
    }
}