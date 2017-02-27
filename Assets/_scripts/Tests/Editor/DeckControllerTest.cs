using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using Controllers;
using Collections;

[TestFixture]
public class DeckControllerTest {

	[Test]
	public void NewDeckIsNotEmpty() {
        var deck = ScriptableObject.CreateInstance<DeckController>();
        var deckObj = NSubstitute.Substitute.For<ICollectionObject>();
        deck.SetCollectionObject(deckObj);
        deck.OnGameStart();
        Assert.IsFalse(deck.IsEmpty);
	}
}
