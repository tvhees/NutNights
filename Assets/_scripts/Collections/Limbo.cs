﻿using System.Collections.Generic;
using Collections.Controllers;

namespace Collections
{
    public class Limbo : Collection
    {
        protected override void Awake()
        {
            Controller = GetManager<LimboController>();
            base.Awake();
        }

        public override void UpdateView(List<Card> cards)
        {
        }
    }
}