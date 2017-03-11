using Collections;
using GameData;
using UnityEngine;

namespace Controllers
{
    [CreateAssetMenu(fileName = "ProphecyController.asset", menuName = "Controllers/Prophecy")]
    public class ProphecyController : CollectionController
    {
        public bool IsFull { get { return Cards.Count >= Constants.prophecySize; } }

        public override void MoveCardTo(ICollectionController target, int index, bool toFront = false)
        {
            Indices.RemoveAt(index);
            CollectionObject.RemoveButton(index);
            base.MoveCardTo(target, index, toFront);
        }

        public override void AddCard(Card card)
        {
            var index = LowestEmptyIndex;
            CollectionObject.AddButton(index);
            Indices.Insert(index, index);
            InsertCard(card, index);
        }
    }
}