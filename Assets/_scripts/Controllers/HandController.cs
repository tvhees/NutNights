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
        public List<int> EmptyIndices;

        public int LowestEmptyIndex {
            get { return EmptyIndices.Count > 0 ? EmptyIndices.Min() : -1; }
        }

        public bool IsFull { get { return Cards.Count >= Constants.handSize; } }

        public override void OnGameStart(params Collection[] dependencies)
        {
            EmptyIndices = new List<int>(new int[5] {0, 1, 2, 3, 4});
            base.OnGameStart(dependencies);
        }

        public void UpdateView(Color keyColor)
        {
            ((Hand) CollectionObject).UpdateView(Cards, keyColor);
        }

        public override void MoveCardTo(ICollectionController target, int index, bool toFront = false)
        {
            EmptyIndices.Add(HandIndexFromRealIndex(index));
            var i = RealIndexFromHandIndex(index);
            CollectionObject.RemoveButton(i);
            base.MoveCardTo(target, i, toFront);
        }

        /// <summary>
        /// Returns the list/transform sibling index corresponding to the given collection position index
        /// </summary>
        private int RealIndexFromHandIndex(int index)
        {
            return index - EmptyIndices.Count(i => i < index);
        }

        private int HandIndexFromRealIndex(int index)
        {
            return index + EmptyIndices.Count(i => i <= index);
        }

        public override void AddCard(Card card)
        {
            var index = LowestEmptyIndex;
            CollectionObject.AddButton(index);
            EmptyIndices.Remove(index);
            InsertCard(card, index);
        }
    }
}