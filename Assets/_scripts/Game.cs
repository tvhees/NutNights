using System;
using System.Collections;
using System.Collections.Generic;
using Collections;
using Controllers;
using GameData;
using RSG;
using UnityEngine;
using UnityEngine.UI;

[ManagerDependency(typeof(ManagerContainer))]
public class Game : BaseMonoBehaviour, IObjectPoolHolder
{
    private HandController handController;
    private GameController gameController;
    private StateController stateController;
    public IObjectPool objectPool;

    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private Toggle discardToggle;
    public Toggle DiscardToggle { get { return discardToggle; } }

    public Constants.CardType LastType { get { return GetManager<LabyrinthController>().LastType; } }

    private void Start()
    {
        gameController = GetManager<GameController>();
        handController = GetManager<HandController>();
        stateController = GetManager<StateController>();
        base.Awake();
        gameController.SetGameObject(this);
    }

    public void NewGame()
    {
        stateController.MoveToState(States.ResetGame);
    }

    public Button CreateCardButton(Transform parent)
    {
        var button = objectPool.GetObject().GetComponent<Button>();
        button.transform.SetParent(parent);
        button.rectTransform().Reset();
        button.rectTransform().sizeDelta = new Vector2(100, 160);
        button.onClick.RemoveAllListeners();
        return button;
    }

    public void CreateCardButton(Transform parent, Card card)
    {
        var button = CreateCardButton(parent).GetComponent<CardButton>();
        button.UpdateView(card, false);
    }

    public void ReturnCardButton(Button button)
    {
        objectPool.ReturnObject(button.gameObject);
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

    public void AddPool(IObjectPool pool)
    {
        objectPool = pool;
    }
}