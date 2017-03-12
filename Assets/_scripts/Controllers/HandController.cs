using System.Collections.Generic;
using Collections;
using GameData;
using UnityEngine;

namespace Controllers
{
    [CreateAssetMenu(fileName = "HandController.asset", menuName = "Controllers/Hand")]
    public class HandController : CollectionController
    {
        public bool IsFull { get { return Cards.Count >= Constants.handSize; } }

        public void UpdateView(Color keyColor)
        {
            ((Hand) CollectionObject).UpdateView(Cards, keyColor);
        }

        public override void MoveCardTo(ICollectionController target, int index, bool toFront = false)
        {
            Indices.RemoveAt(index);
            CollectionObject.GetButton(index).MoveToNewCollection(target.CollectionObject);
            base.MoveCardTo(target, index, toFront);
        }

        public override void AddCard(Card card)
        {
            var index = LowestEmptyIndex;

            CollectionObject
                .AddButton(index)
                .Grow();

            Indices.Insert(index, index);
            InsertCard(card, index);
        }
    }
}