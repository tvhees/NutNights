using System.Collections.Generic;
using Controllers;
using GameData;
using UnityEngine.UI;

namespace Collections
{
    public class Limbo : Collection
    {
        protected override void Awake()
        {
            Controller = GetManager<LimboController>();
            base.Awake();
        }

        public override Button AddButton(int index)
        {
            throw new System.NotImplementedException();
        }

        public override void UpdateView(List<Card> cards)
        {
        }
    }
}