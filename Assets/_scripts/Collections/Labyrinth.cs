using System.Collections.Generic;
using Components;
using Controllers;
using GameData;
using UnityEngine;
using UnityEngine.UI;

namespace Collections
{
    public class Labyrinth : Collection
    {
        public Path HoldPath;

        protected override void Awake()
        {
            Controller = GetManager<LabyrinthController>();
            base.Awake();
        }

        public override Button AddButton(Button newButton, int index = 0)
        {
            ShiftExistingButtons();
            newButton.interactable = false;
            newButton.transform.SetParent(this.transform);
            newButton.LocalMoveTo(HoldPath.GetPoint(1.0f));
            HoldPath.AddPoint(50 * Vector3.left);
            return newButton;
        }

        private void ShiftExistingButtons()
        {
            var buttons = GetComponentsInChildren<Button>();
            foreach (var btn in buttons)
            {
                btn.LocalMoveTo(HoldPath.GetPoint(btn.transform.GetSiblingIndex() + 1))
                    .Rotate(360 * Vector3.forward);
            }
        }

        public override void UpdateView(List<Card> cards)
        {
            if (HoldPath.Points.Length - transform.childCount > 2)
            {
                HoldPath.Reset();
            }
        }
    }
}