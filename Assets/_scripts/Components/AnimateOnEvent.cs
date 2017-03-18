using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Components
{
    [RequireComponent(typeof(Animator))]
    public class AnimateOnEvent : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
    {
        public PropType PropertyType;
        public string PropertyName;
        public bool Toggle;
        public bool BoolValue;
        public float FloatValue;
        public bool WhenPointerEnters;
        public bool WhenPointerClicks;
        public bool UseCustomEvent;
        public UnityEvent CustomEvent = new UnityEvent();
        private Animator animator;

        public enum PropType { Trigger, Bool, Float }

        private void Awake()
        {
            animator = GetComponent<Animator>();
            CustomEvent.AddListener(OnCustomEvent);
        }

        protected void SetProperty()
        {
            if (PropertyType == PropType.Trigger)
            {
                animator.SetTrigger(PropertyName);
            }

            if (PropertyType == PropType.Float)
            {
                animator.SetFloat(PropertyName, FloatValue);
            }

            if (PropertyType == PropType.Bool)
            {
                if (Toggle)
                {
                    animator.SetBool(PropertyName, !animator.GetBool(PropertyName));
                }
                else
                {
                    animator.SetBool(PropertyName, BoolValue);
                }
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!WhenPointerEnters)
            {
                return;
            }

            SetProperty();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!WhenPointerClicks)
            {
                return;
            }

            SetProperty();
        }

        public void OnCustomEvent()
        {
            if (UseCustomEvent)
            {
                SetProperty();
            }
        }
    }
}