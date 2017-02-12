using UnityEngine;
using UnityEngine.UI;

namespace Components
{
    public class SpriteOutline : MonoBehaviour {

        public Color OutlineColor = Color.white;
        [Range(1,20)]
        public int OutlineDistance = 1;
        private Image image;

        private void OnEnable() {
            image = GetComponent<Image>();

            UpdateOutline(true);
        }

        private void OnDisable() {
            UpdateOutline(false);
        }

        private void Update() {
            UpdateOutline(true);
        }

        private void UpdateOutline(bool outline) {
            var mat = Instantiate(image.material);
            mat.SetFloat("_Outline", outline ? 1f : 0);
            mat.SetFloat("_OutlineDistance", OutlineDistance);
            mat.SetColor("_OutlineColor", OutlineColor);
            image.material = mat;
        }
    }
}