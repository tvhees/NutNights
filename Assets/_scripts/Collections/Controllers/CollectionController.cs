using System.Collections.Generic;
using UnityEngine.Assertions;

namespace Collections.Controllers
{
    public abstract class CollectionController : Manager
    {
        protected ICollectionObject CollectionObject;

        protected List<Card> Cards;
        public bool IsEmpty { get { return Cards.Count <= 0; } }

        public virtual void OnGameStart(params Collection[] dependencies)
        {
            Cards = new List<Card>();
        }

        public void SetCollectionObject(ICollectionObject collectionObject)
        {
            this.CollectionObject = collectionObject;
        }

        public virtual void ResetCollection()
        {
            Cards.Clear();
            CollectionObject.ResetCollection();
        }

        public void MoveCardTo(CollectionController target, bool toFront = false)
        {
            MoveCardTo(target, 0, toFront);
        }

        public void MoveCardTo(CollectionController target, int index, bool toFront = false)
        {
            var card = GetCard(index);
            if (toFront)
                target.InsertCard(card);
            else
                target.AddCard(card);
            Cards.Remove(card);
            UpdateView();
        }

        public void MoveCardTo(CollectionController target, Card cardDef)
        {
            if (IsEmpty)
                return;
            var index = Cards.IndexOf(cardDef);
            Assert.IsFalse(index < 0, index + " " + cardDef.color + " " + cardDef.type);
            MoveCardTo(target, index);
        }

        public void MoveAllTo(CollectionController target, bool toFront = false)
        {
            if (target == this)
                return;

            while (!IsEmpty)
                MoveCardTo(target, toFront);
        }

        public void MoveCardsTo(CollectionController target, int number)
        {
            if (target == this)
                return;

            for (int i = 0; i < number; i++)
            {
                if (IsEmpty)
                    break;

                MoveCardTo(target);
            }
        }

        public bool HasCard(Card card)
        {
            return Cards.Contains(card);
        }

        public Card GetCard()
        {
            return GetCard(0);
        }

        public Card GetCard(int index)
        {
            return Cards[index];
        }

        public void AddCard(Card card)
        {
            Cards.Add(card);
            UpdateView();
        }

        public void InsertCard(Card card)
        {
            Cards.Insert(0, card);
            UpdateView();
        }

        public void SwapCards(int i, int j)
        {
            var temp = Cards[i];
            Cards[i] = Cards[j];
            Cards[j] = temp;
            UpdateView();
        }

        public virtual void UpdateView()
        {
            CollectionObject.UpdateView(Cards);
        }

        public void Shuffle()
        {
            Cards.Shuffle();
        }
    }
}