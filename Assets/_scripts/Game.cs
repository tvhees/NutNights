using System;
using System.Collections;
using System.Collections.Generic;
using Collections;
using Collections.Controllers;
using RSG;
using UnityEngine;
using UnityEngine.UI;

public interface ICardController
{
    Toggle DiscardToggle { get; }
    Button CreateCardButton(Transform parent);
    void CreateCardButton(Transform parent, Card card);
}

[ManagerDependency(typeof(ManagerContainer))]
public class Game : BaseMonoBehaviour, ICardController
{
    private HandController hand;

    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private Toggle discardToggle;
    public Toggle DiscardToggle { get { return discardToggle; } }

    public bool FullHand { get { return GetManager<HandController>().IsFull; } }
    public Constants.CardType LastType { get { return GetManager<LabyrinthController>().LastType; } }

    public void NewGame()
    {
        GetManager<GameController>().NewGame();
    }

    public Button CreateCardButton(Transform parent)
    {
        var button = (Instantiate(buttonPrefab, parent) as GameObject).GetComponent<Button>();
        button.rectTransform().Reset();
        return button;
    }

    public void CreateCardButton(Transform parent, Card card)
    {
        var button = CreateCardButton(parent).GetComponent<CardButton>();
        button.UpdateView(card, false);
    }

    public void OnDiscardToggled(Toggle discardToggle)
    {
        Flags.isDiscarding = discardToggle.isOn;
        hand.UpdateView();
    }

    public void OnCompleteProphecyPressed()
    {
        GetManager<GameController>().OnCompleteProphecyPressed();
    }

    public void OnDiscardHandPressed()
    {
        GetManager<GameController>().OnDiscardHandPressed();
    }

    public void OnDoorCardPressed(int i)
    {
        GetManager<GameController>().OnDoorCardPressed(i);
    }

    public void OnDiscardFromDeckPressed()
    {
        GetManager<GameController>().OnDiscardFromDeckPressed();
    }
}