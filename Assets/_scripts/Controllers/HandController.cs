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

        public override void OnGameStart(params Collection[] dependencies)
        {
            ((Hand)CollectionObject).AddButtons();
            base.OnGameStart(dependencies);
        }

        public void UpdateView(Color keyColor)
        {
            ((Hand) CollectionObject).UpdateView(Cards, keyColor);
        }
    }
}