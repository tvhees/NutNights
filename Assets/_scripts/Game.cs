using System;
using System.Collections;
using System.Collections.Generic;
using Collections;
using Collections.Controllers;
using RSG;
using UnityEngine;
using UnityEngine.UI;

[ManagerDependency(typeof(ManagerContainer))]
public class Game : BaseMonoBehaviour
{
    private HandController handController;
    private GameController gameController;

    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private Toggle discardToggle;
    public Toggle DiscardToggle { get { return discardToggle; } }

    public bool FullHand { get { return GetManager<HandController>().IsFull; } }
    public Constants.CardType LastType { get { return GetManager<LabyrinthController>().LastType; } }

    private void Start()
    {
        gameController = GetManager<GameController>();
        handController = GetManager<HandController>();
        base.Awake();
        gameController.SetGameObject(this);
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
        handController.UpdateView();
    }

    public void OnCompleteProphecyPressed()
    {
        gameController.OnCompleteProphecyPressed();
    }

    public void OnDiscardHandPressed()
    {
        gameController.OnDiscardHandPressed();
    }

    public void OnDoorCardPressed(int i)
    {
        gameController.OnDoorCardPressed(i);
    }

    public void OnDiscardFromDeckPressed()
    {
        gameController.OnDiscardFromDeckPressed();
    }
}