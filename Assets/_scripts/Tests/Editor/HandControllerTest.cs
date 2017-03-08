using System.Collections.Generic;
using Collections;
using Controllers;
using GameData;
using JetBrains.Annotations;
using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    [TestFixture]
    public class HandControllerTest
    {
        private HandController hand;

        [SetUp]
        public void Setup()
        {
            hand = ScriptableObject.CreateInstance<HandController>();
            var handObj = NSubstitute.Substitute.For<ICollectionObject>();
            hand.SetCollectionObject(handObj);
            hand.OnGameStart();
        }

        [TearDown]
        public void TearDown()
        {
            Object.DestroyImmediate(hand);
        }

        [Test]
        public void CardsAddedInCorrectSequence()
        {
            for(var i = 0; i < Constants.handSize; i++)
            {
                Assert.IsTrue(hand.LowestEmptyIndex == i);
                hand.AddCard(new Card());
            }
        }

        [Test]
        public void EmptyIndexMatchesRemovedCard()
        {
            for(var i = 0; i < Constants.handSize; i++)
            {
                hand.AddCard(new Card());
            }
            var target = NSubstitute.Substitute.For<ICollectionController>();
            var index = 2;
            hand.MoveCardTo(target, index);
            Assert.AreEqual(index, hand.LowestEmptyIndex);
        }

        [Test]
        public void MovingAllCardsCreatesCorrectEmptyIndexArray()
        {
            for(var i = 0; i < Constants.handSize; i++)
            {
                hand.AddCard(new Card());
            }
            Assert.IsTrue(hand.LowestEmptyIndex == Constants.handSize);

            var target = NSubstitute.Substitute.For<ICollectionController>();
            hand.MoveAllTo(target);
            Assert.IsTrue(hand.LowestEmptyIndex == 0);
            Assert.IsTrue(hand.Indices.Count == 0);
        }
    }
}