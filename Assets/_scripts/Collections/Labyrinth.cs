using System;
using System.Collections.Generic;
using Components;
using Controllers;
using DG.Tweening;
using GameData;
using UnityEngine;
using UnityEngine.UI;

namespace Collections
{
    public class Labyrinth : Collection
    {
        [SerializeField]
        private Path holdPath;

        [SerializeField] [Range(0.1f, 5f)]
        private float dropSpeed = 1.0f;
        [SerializeField] [Range(0.1f, 5f)]
        private float rollSpeed = 1.0f;

        protected override void Awake()
        {
            Controller = GetManager<LabyrinthController>();
            base.Awake();
        }

        public override Button AddButton(Button newButton, int index = 0)
        {
            newButton.interactable = false;
            newButton.transform.SetParent(this.transform);
            DropButton(newButton)
                .Append(ShiftButtonsAcross());
            holdPath.AddPoint(50 * Vector3.left);
            return newButton;
        }

        private Sequence DropButton(Button button)
        {
            var seq = DOTween.Sequence();
            var trans = button.transform;
            var point = holdPath.GetPoint(1.0f);
            var duration = 1.0f / (dropSpeed * Constants.animationSpeed);
            seq.Append(trans.DOLocalMoveY(point.y, duration).SetEase(Ease.OutBounce));
            return seq;
        }

        private Sequence ShiftButtonsAcross()
        {
            var seq = DOTween.Sequence();
            var buttons = GetComponentsInChildren<Button>();
            foreach (var btn in buttons)
            {
                var point = holdPath.GetPoint(btn.transform.GetSiblingIndex() + 1);
                var direction = Mathf.Sign(btn.transform.localPosition.x - point.x);
                var duration = 1.0f / (rollSpeed * Constants.animationSpeed);
                seq.Insert(0, btn.transform.DOLocalMove(point, duration))
                    .Insert(0, btn.transform.DORotate(direction * 360 * Vector3.forward, duration, RotateMode.FastBeyond360));
            }
            return seq;
        }

        public override void UpdateView(List<Card> cards)
        {
            if (transform.childCount == 0)
            {
                holdPath.Reset();
            }
        }
    }
}