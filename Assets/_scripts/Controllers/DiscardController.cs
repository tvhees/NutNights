using System.Collections.Generic;
using Collections;
using GameData;
using UnityEngine;

namespace Controllers
{
    [CreateAssetMenu(fileName = "DiscardController.asset", menuName = "Controllers/Discard")]
    public class DiscardController : CollectionController
    {
        public override void OnGameStart(params Collection[] dependencies)
        {
            Cards = new List<Card>();
        }
    }
}