using System;
using System.Collections.Generic;
using Collections;
using GameData;
using UnityEngine;
using UnityEngine.Assertions;

namespace Controllers
{
    public interface ICollectionController
    {
        void AddCard(Card card);
        void InsertCard(Card card, int index = 0);
    }

    public abstract class CollectionController : Manager, ICollectionController
    {
        protected ICollectionObject CollectionObject;

        protected List<Card> Cards = new List<Card>();

        public bool IsEmpty
        {
            get { return Cards.Count <= 0; }
        }

        public int Size
        {
            get { return Cards.Count; }
        }

        public virtual void OnGameStart(params Collection[] dependencies)
        {
            Cards = new List<Card>();
            UpdateView();
        }

        public void SetCollectionObject(ICollectionObject collectionObject)
        {
            this.CollectionObject = collectionObject;
        }

        public void MoveFirstCardTo(ICollectionController target, bool toFront = false)
        {
            MoveCardTo(target, 0, toFront);
        }

        public virtual void MoveCardTo(ICollectionController target, int index, bool toFront = false)
        {
            var card = GetCard(index);
            if (toFront)
            {
                target.InsertCard(card);
            }
            else
            {
                target.AddCard(card);
            }
            Cards.Remove(card);
            UpdateView();
        }

        public void MoveCardTo(ICollectionController target, Card cardDef)
        {
            if (IsEmpty)
                return;
            var index = Cards.IndexOf(cardDef);
            Assert.IsFalse(index < 0, index + " " + cardDef.color + " " + cardDef.type);
            MoveCardTo(target, index);
        }

        public void MoveAllTo(ICollectionController target, bool toFront = false)
        {
            if (ReferenceEquals(target, this))
            {
                return;
            }

            var n = Cards.Count - 1;

            for (var i = n; i >= 0; i--)
            {
                MoveCardTo(target, i, toFront);
            }
        }

        public void MoveCardsTo(ICollectionController target, int number)
        {
            if (ReferenceEquals(target, this))
                return;

            for (var i = 0; i < number; i++)
            {
                if (IsEmpty)
                    break;

                MoveFirstCardTo(target);
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

        public virtual void AddCard(Card card)
        {
            Cards.Add(card);
            UpdateView();
        }

        public virtual void InsertCard(Card card, int index = 0)
        {
            Cards.Insert(index, card);
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