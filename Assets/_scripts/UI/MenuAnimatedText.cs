using GameData;
using DG.Tweening;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace GameData
{
    public partial class GlobalSettings
    {
        [SerializeField]
        private MenuAnimatedText.Settings menuTextSettings;
        public static MenuAnimatedText.Settings MenuTextSettings { get { return instance.menuTextSettings; } }
    }
}

namespace UI
{
    public class MenuAnimatedText : MonoBehaviour
    {
        [SerializeField] private bool isAnimated;
        [SerializeField] private string fullText;
        [SerializeField] private Text text;
        [SerializeField] private Outline outline;

        private Tweener txtTweener;

        [System.Serializable]
        public class Settings
        {
            [PaletteColor] public Color TextColor;
            [PaletteColor] public Color OutlineColor;
            [Range(0.1f, 5f)]
            public float AnimationSpeed;
            public float AnimationTime {get { return 1 / AnimationSpeed; } }
        }

        private void Start()
        {
            GlobalSettings.OnChanged.AddListener(UpdateSettings);
            UpdateSettings();
        }

        private void UpdateSettings()
        {
            text = text ? text : GetComponent<Text>();
            if (text)
            {
                text.color = GlobalSettings.MenuTextSettings.TextColor;
                if (isAnimated)
                {
                    AnimateText();
                }
                else
                {
                    text.text = fullText;
                }
            }

            outline = outline ? outline : GetComponent<Outline>();
            if (outline)
            {
                outline.effectColor = GlobalSettings.MenuTextSettings.OutlineColor;
            }
        }

        private void AnimateText()
        {
            text.text = "";
            txtTweener.Kill();
            txtTweener = text.DOText(fullText, GlobalSettings.MenuTextSettings.AnimationTime)
                .SetDelay(2f * GlobalSettings.MenuGfxSettings.AnimationTime);
        }
    }
}