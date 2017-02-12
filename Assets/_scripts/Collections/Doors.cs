using System.Collections.Generic;
using Controllers;
using GameData;
using UnityEngine;
using UnityEngine.UI;

namespace Collections
{
    public class Doors : Collection
    {
        [SerializeField] private GameObject[] holders;
        [SerializeField] private GameObject holderPrefab;

        protected override void Awake()
        {
            Controller = GetManager<DoorsController>();
            base.Awake();
            AddHolders();
        }

        private void AddHolders()
        {
            if (transform.childCount > 0)
                return;

            holders = new GameObject[Constants.colors.Count];

            for (var i = 0; i < holders.Length; i++)
            {
                holders[i] = Instantiate(holderPrefab, transform) as GameObject;
                holders[i].rectTransform().Reset();
            }
        }

        public override void UpdateView(List<Card> cards)
        {
            var buttons = GetComponentsInChildren<Button>();

            for (var i = buttons.Length; i < cards.Count; i++)
            {

                var holderIndex = Constants.colors.IndexOf(cards[i].color);
                game.CreateCardButton(holders[holderIndex].transform, cards[i]);
            }
        }
    }
}