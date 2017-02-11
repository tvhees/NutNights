using UnityEngine;

namespace Collections.Controllers
{
    [CreateAssetMenu(fileName = "HandController.asset", menuName = "Controllers/Hand")]
    public class HandController : CollectionController
    {
        public bool IsFull { get { return Cards.Count >= Constants.handSize; } }

        public void UpdateView(Color keyColor)
        {
            ((Hand) CollectionObject).UpdateView(Cards, keyColor);
        }
    }
}