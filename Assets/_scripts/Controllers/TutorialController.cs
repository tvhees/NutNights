using System;
using RSG;
using UI;
using UnityEngine;

namespace Controllers
{
    [CreateAssetMenu(fileName = "TutorialController.asset", menuName = "Controllers/Tutorial")]
    [ManagerAlwaysGlobal]
    public class TutorialController : Manager
    {
        private TutorialMessage message;
        private readonly PromiseTimer delayTimer = new PromiseTimer();
        private readonly PromiseTimer waitTimer = new PromiseTimer();
        private bool isWaitingForPointer;

        public override void OnUpdate()
        {
            delayTimer.Update(Time.deltaTime);
            waitTimer.Update(Time.deltaTime);
        }

        public void SetMessageObject(TutorialMessage message)
        {
            this.message = message;
        }

        public void OnPointerDown()
        {
            isWaitingForPointer = false;
        }

        public IPromise StartTutorial()
        {
            return Promise.Sequence(
                DisplayMessage("Welcome to Nut Nights!", Vector2.zero),
                Wait(),
                DisplayMessage("This is a tutorial message"),
                Wait(),
                HideMessage()
            );
        }

        private Func<IPromise> DisplayMessage(string text)
        {
            return () =>
            {
                message.Display(text);
                return Promise.Resolved();
            };
        }

        public Func<IPromise> DisplayMessage(string text, Vector2 position)
        {
            return () =>
            {
                message.Display(text, position);
                return Promise.Resolved();
            };
        }

        public Func<IPromise> HideMessage()
        {
            return () =>
            {
                message.Hide();
                return Promise.Resolved();
            };
        }

        public Func<IPromise> Wait()
        {
            return () =>
            {
                isWaitingForPointer = true;
                return waitTimer.WaitWhile(_ => isWaitingForPointer);
            };
        }
    }
}