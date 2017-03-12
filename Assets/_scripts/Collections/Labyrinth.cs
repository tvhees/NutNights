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

        public override Button AddButton(Button button, int index = 0)
        {
            ShiftButtons();
            var btn = base.AddButton(button, index);
            btn.rectTransform().localPosition = HoldPath.GetPoint(0);
            return btn;
        }

        private void ShiftButtons()
        {
            HoldPath.AddPoint(50 * Vector3.left);
            var buttons = GetComponentsInChildren<Button>();
            foreach (var btn in buttons)
            {
                Debug.Log(btn.transform.GetSiblingIndex());
                btn.rectTransform().localPosition = HoldPath.GetPoint(btn.transform.GetSiblingIndex() + 1);
            }
        }

        public override void UpdateView(List<Card> cards)
        {
            // Left blank
        }
    }
}