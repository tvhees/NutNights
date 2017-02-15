using System;
using Collections;
using GameData;
using UnityEngine;

namespace Controllers
{
    [CreateAssetMenu(fileName = "HandController.asset", menuName = "Controllers/Hand")]
    public class HandController : CollectionController
    {
        public bool IsFull { get { return Cards.Count >= Constants.handSize; } }

        public override void ResetCollection()
        {
            // Left empty - don't want to delete children
        }

        public void UpdateView(Color keyColor)
        {
            ((Hand) CollectionObject).UpdateView(Cards, keyColor);
        }
    }
}