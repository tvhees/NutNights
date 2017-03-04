using Collections;
using Controllers;
using GameData;
using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    [TestFixture]
    public class DeckControllerTest
    {
        private DeckController deck;

        [TestFixtureSetUp]
        public void Setup()
        {
            deck = ScriptableObject.CreateInstance<DeckController>();
            var deckObj = NSubstitute.Substitute.For<ICollectionObject>();
            deck.SetCollectionObject(deckObj);
            deck.OnGameStart();
        }

        [Test]
        public void NewDeckIsNotEmpty()
        {
            Assert.IsFalse(deck.IsEmpty);
        }

        [Test]
        public void MoveCardDecreasesDeckSizeAndRemovesFirstCard()
        {
            var target = NSubstitute.Substitute.For<ICollectionController>();
            var size = deck.Size;
            var card = deck.GetCard();
            deck.MoveCardTo(target);
            Assert.IsTrue(deck.Size == size - 1);
            Assert.IsFalse(deck.GetCard() == card);
        }

        [Test]
        public void AddCardIncreasesDeckSize()
        {
            var card = new Card();
            var size = deck.Size;
            deck.AddCard(card);
            Assert.IsTrue(deck.Size == size + 1);
            Assert.IsTrue(deck.GetCard(size) == card);

        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            Object.DestroyImmediate(deck);
        }
    }
}
