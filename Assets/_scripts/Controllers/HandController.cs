using System.Collections.Generic;
using Collections;
using GameData;
using UnityEngine;

namespace Controllers
{
    [CreateAssetMenu(fileName = "HandController.asset", menuName = "Controllers/Hand")]
    public class HandController : CollectionController
    {
        public List<int> Indices { get; private set; }

        public int LowestEmptyIndex {
            get
            {
                for (var i = 0; i < Indices.Count; i++)
                {
                    if (Indices[i] > i)
                    {
                        return i;
                    }
                }
                return Indices.Count;
            }
        }

        public bool IsFull { get { return Cards.Count >= Constants.handSize; } }

        public override void OnGameStart(params Collection[] dependencies)
        {
            Indices = new List<int>();
            base.OnGameStart(dependencies);
        }

        public void UpdateView(Color keyColor)
        {
            ((Hand) CollectionObject).UpdateView(Cards, keyColor);
        }

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