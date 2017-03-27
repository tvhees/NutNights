using Controllers;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    [ManagerDependency(typeof(ManagerContainer))]
    public class TutorialMessage : BaseMonoBehaviour, IPointerDownHandler
    {
        [SerializeField]
        private Text message;
        private Tweener textTween;
        private Sequence tweenSequence;

        protected override void Awake()
        {
            base.Awake();
            GetManager<TutorialController>().SetMessageObject(this);
            gameObject.SetActive(false);
        }

        public void Display(string text)
        {
            Display(text, this.rectTransform().anchoredPosition);
        }

        public void Display(string text, Vector2 position)
        {
            textTween.Kill();
            tweenSequence.Kill();
            message.text = "";

            this.rectTransform().anchoredPosition = position;
            gameObject.SetActive(true);

            textTween = message.DOText(text, 1.0f)
                .OnComplete(() => AppendDotSequence(text));
        }

        private void AppendDotSequence(string text)
        {
            message.text = text;
            tweenSequence = DOTween.Sequence()
                .Append(message.DOText(text + " ...", 0.5f))
                .AppendInterval(1f);
            tweenSequence.SetLoops(-1);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            GetManager<TutorialController>().OnPointerDown();
        }
    }
}
