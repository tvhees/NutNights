using System;
using System.Collections.Generic;
using System.Linq;
using Collections;
using GameData;
using UnityEngine;

namespace Controllers
{
    [CreateAssetMenu(fileName = "HandController.asset", menuName = "Controllers/Hand")]
    public class HandController : CollectionController
    {
        public readonly List<int> EmptyIndices = new List<int>(new int[5]{ 0, 1, 2, 3, 4 });

        public int LowestEmptyIndex {
            get { return EmptyIndices.Count > 0 ? EmptyIndices.Min() : -1; }
        }
        public bool IsFull { get { return Cards.Count >= Constants.handSize; } }

        public void UpdateView(Color keyColor)
        {
            ((Hand) CollectionObject).UpdateView(Cards, keyColor);
        }

        public override void MoveCardTo(ICollectionController target, int index, bool toFront = false)
        {
            EmptyIndices.Add(index);
            CollectionObject.RemoveButton(index);
            base.MoveCardTo(target, index, toFront);
        }

        public override void AddCard(Card card)
        {
            var index = LowestEmptyIndex;
            CollectionObject.AddButton(index);
            EmptyIndices.Remove(index);
            base.AddCard(card);
        }
    }
}