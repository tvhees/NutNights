using Controllers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [ManagerDependency(typeof(ManagerContainer))]
    public class TutorialMessage : BaseMonoBehaviour
    {
        [SerializeField]
        private Text message;

        protected override void Awake()
        {
            base.Awake();
            GetManager<TutorialController>().SetMessageObject(this);
            gameObject.SetActive(false);
        }

        public void Display(string text, Vector2 position)
        {
            gameObject.SetActive(true);
            message.text = text;
            this.rectTransform().anchoredPosition = position;
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
