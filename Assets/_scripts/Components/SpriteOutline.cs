using System;
using UnityEngine;
using UnityEngine.UI;

namespace Components
{
    public class SpriteOutline : MonoBehaviour {

        public Color OutlineColor = Color.white;
        public Color AlphaMultiplier = Color.white;
        public Vector2 OutlineDirection;
        [Range(1,20)]
        public int OutlineDistance = 1;
        private Image image;
        private Outline outline;

        private void OnEnable() {
            image = GetComponent<Image>();
            outline = GetComponent<Outline>();
            UpdateOutline(true);
        }

        private void OnDisable() {
            UpdateOutline(false);
        }

        private void Update() {
            UpdateOutline(true);
        }

        private void UpdateOutline(bool showOutline) {
            outline.enabled = showOutline;
            outline.effectDistance = OutlineDistance * OutlineDirection;
            outline.effectColor = OutlineColor * AlphaMultiplier;
        }

        [Obsolete]
        public void UpdateMatOutline(bool showOutline) {
            var mat = Instantiate(image.material);
            mat.SetFloat("_Outline", showOutline ? 1f : 0);
            mat.SetFloat("_OutlineDistance", OutlineDistance);
            mat.SetColor("_OutlineColor", OutlineColor);
            image.material = mat;
        }
    }
}