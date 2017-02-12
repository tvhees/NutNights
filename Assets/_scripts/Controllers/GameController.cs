using System;
using GameData;
using UnityEngine;

namespace Controllers
{
    [CreateAssetMenu(fileName = "GameController.asset", menuName = "Controllers/Game")]
    [ManagerDependency(typeof(DeckController))]
    [ManagerDependency(typeof(HandController))]
    [ManagerDependency(typeof(DiscardController))]
    [ManagerDependency(typeof(LabyrinthController))]
    [ManagerDependency(typeof(LimboController))]
    [ManagerDependency(typeof(DoorsController))]
    [ManagerDependency(typeof(ProphecyController))]
    [ManagerDependency(typeof(StateController))]
    public class GameController : Manager
    {
        private Game gameObj;
        private DeckController deck;
        private HandController hand;
        private DiscardController discard;
        private LabyrinthController labyrinth;
        private LimboController limbo;
        private DoorsController doors;
        private ProphecyController prophecy;
        private StateController state;

        public bool Won { get { return doors.DoorsAcquired >= Constants.colors.Count * (int)Constants.CardType.Squirrel; } }
        public bool Lost { get { return deck.IsEmpty; } }

        public void SetGameObject(Game gameObj)
        {
            this.gameObj = gameObj;
            NewGame();
        }

        public void NewGame()
        {
            GetManagers();
            state.MoveToState(States.NewGame);
        }

        public void GetManagers()
        {
            deck = GetManager<DeckController>();
            hand = GetManager<HandController>();
            labyrinth = GetManager<LabyrinthController>();
            doors = GetManager<DoorsController>();
            limbo = GetManager<LimboController>();
            discard = GetManager<DiscardController>();
            prophecy = GetManager<ProphecyController>();
            state = GetManager<StateController>();
            state.SetGameController(this);
        }

        public void StartGame()
        {
            deck.OnGameStart();
            hand.OnGameStart();
            labyrinth.OnGameStart();
            doors.OnGameStart();
            limbo.OnGameStart();
            discard.OnGameStart();
            prophecy.OnGameStart();
        }

        public void DrawCardRecursive()
        {
            do
            {
                DrawCard();
                if (deck.IsEmpty)
                {
                    state.MoveToState(States.CheckingWinLoss);
                }
            } while (state.IsInCleanup && !hand.IsFull);

            EmptyLimbo();
        }

        private void DrawCard()
        {
            var card = deck.GetCard();
            switch (card.type)
            {
                case Constants.CardType.Acorn:
                case Constants.CardType.Hickory:
                case Constants.CardType.Almond:
                    deck.MoveCardTo(hand);
                    break;
                case Constants.CardType.Squirrel:
                    TriggerDoor(card.color);
                    break;
                case Constants.CardType.Nightmare:
                    TriggerNightmare();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void TriggerDoor(Color color)
        {
            if (!hand.HasCard(Card.Key(color)) || Flags.recycleDreamsAndDoors)
            {
                deck.MoveCardTo(limbo);
                return;
            }

            deck.MoveCardTo(prophecy);
            state.MoveToState(States.Door);
            hand.UpdateView(color);
        }

        public void TriggerNightmare()
        {
            if (Flags.recycleDreamsAndDoors)
            {
                deck.MoveCardTo(limbo);
                return;
            }

            deck.MoveCardTo(prophecy);
            state.MoveToState(States.Nightmare);
            hand.UpdateView();
        }

        public void EmptyLimbo()
        {
            limbo.EmptyTo(deck);
            if (state.IsInCleanup)
            {
                state.MoveToState(States.WaitingForPlayer);
            }
        }

        public void OnHandCardPressed(int i)
        {
            var card = hand.GetCard(i);
            if (Flags.isDiscarding)
            {
                gameObj.DiscardToggle.isOn = false;
                if (card.type == Constants.CardType.Almond)
                    TriggerProphecy(i);
                else
                    DiscardCardFromHand(i);
            }
            else if (Flags.isInNightmare)
                DiscardKeyToNightmare(i);
            else if (Flags.doorDrawn)
                DiscardKeyToDoor(i, card.color);
            else
                PlayCardToLabyrinth(i);
        }

        private void TriggerProphecy(int i)
        {
            while (!prophecy.IsFull && !deck.IsEmpty)
                deck.MoveCardTo(prophecy);
            hand.MoveCardTo(discard, i);
            state.MoveToState(States.Prophecy);
        }

        private void DiscardCardFromHand(int i)
        {
            hand.MoveCardTo(discard, i);
            state.MoveToState(States.CheckingWinLoss);
        }

        public void DiscardKeyToNightmare(int i)
        {
            state.MoveToState(States.ProcessingTurn);
            hand.MoveCardTo(discard, i);
            EndNightmare();
        }

        public void EndNightmare()
        {
            prophecy.MoveCardTo(discard, Card.Nightmare);
            state.MoveToState(States.CheckingWinLoss);
        }

        private void DiscardKeyToDoor(int i, Color color)
        {
            state.MoveToState(States.ProcessingTurn);
            hand.MoveCardTo(discard, i);
            doors.GetDoorFrom(prophecy, color);
            state.MoveToState(States.CheckingWinLoss);
        }

        private void PlayCardToLabyrinth(int i)
        {
            hand.MoveCardTo(labyrinth, i);
            if (labyrinth.TripletCompleted())
                GetDoorFromDeck(labyrinth.LastColor);
            state.MoveToState(States.CheckingWinLoss);
        }

        private void GetDoorFromDeck(Color color)
        {
            doors.GetDoorFrom(deck, color);
            deck.Shuffle();
        }

        public void OnDiscardHandPressed()
        {
            state.MoveToState(States.ProcessingTurn);
            hand.MoveAllTo(discard);
            Flags.recycleDreamsAndDoors = true;
            EndNightmare();
        }

        public void OnDoorCardPressed(int i)
        {
            state.MoveToState(States.ProcessingTurn);
            doors.MoveCardTo(limbo, i);
            EndNightmare();
        }

        public void OnDiscardFromDeckPressed()
        {
            state.MoveToState(States.ProcessingTurn);
            deck.MoveCardsTo(discard, limbo, Constants.nightmareSize);
            EndNightmare();
        }

        public void OnCompleteProphecyPressed()
        {
            state.MoveToState(States.ProcessingTurn);
            if (Flags.doorDrawn)
                prophecy.MoveAllTo(limbo);
            else
                prophecy.MoveAllTo(deck, true);
            state.MoveToState(States.CheckingWinLoss);
        }

        public void OnProphecyCardPressed(int i)
        {
            state.MoveToState(States.ProcessingTurn);
            if (prophecy.GetCard(i).type == Constants.CardType.Squirrel)
                prophecy.MoveCardTo(limbo, i);
            else
                prophecy.MoveCardTo(discard, i);
            state.MoveToState(States.CompleteProphecy);
        }
    }
}