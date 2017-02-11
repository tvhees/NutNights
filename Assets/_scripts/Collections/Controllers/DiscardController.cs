using System.Collections.Generic;
using UnityEngine;

namespace Collections.Controllers
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