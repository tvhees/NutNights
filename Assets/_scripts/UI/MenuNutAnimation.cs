using Components;
using GameData;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;
using UI;

namespace GameData {
    public partial class GlobalSettings {
        [SerializeField]
        private MenuNutAnimation.MenuGraphicsSettings menuGfxSettings;
        public static MenuNutAnimation.MenuGraphicsSettings MenuGfxSettings { get { return instance.menuGfxSettings; } }
    }
}

namespace UI {
    public class MenuNutAnimation : MonoBehaviour {

        [SerializeField]
        private Image screenCover;
        [SerializeField]
        private Path[] paths;
        [SerializeField]
        private Image[] nuts;

        private Sequence startSequence;

        [System.Serializable]
        public class MenuGraphicsSettings {
            [Range(0.1f, 5f)]
            public float AnimationSpeed;
            public float AnimationTime { get { return 1f / AnimationSpeed; } }
        }

        private void Start()
        {
            GlobalSettings.OnChanged.AddListener(() => {
                Reset();
                RunMenuAnimation();
            });

            Reset();
            RunMenuAnimation();
        }

        private void RunMenuAnimation()
        {
            var duration = GlobalSettings.MenuGfxSettings.AnimationTime;
            startSequence = DOTween.Sequence()
                .Append(screenCover.DOColor(Color.clear, duration));

            for (var i = 0; i < paths.Length; i++)
            {
                startSequence.Insert(duration, DropAndRollNut(i, duration));
            }
        }

        private Sequence DropAndRollNut(int i, float duration)
        {
            var points = paths[i].Points;
            var nut = nuts[i];
            var direction = Mathf.Sign(points[1].x - points.Last().x);
            return DOTween.Sequence().Append(nut.rectTransform.DOLocalMove(points[1], duration).SetEase(Ease.OutBounce))
                .Append(nut.rectTransform.DOLocalMove(points.Last(), duration))
                .Insert(duration, nut.transform.DORotate(direction * 360 * Vector3.forward, duration, RotateMode.FastBeyond360));
        }

        private void Reset()
        {
            screenCover.color = Color.black;
            for (var i = 0; i < paths.Length; i++)
            {
                nuts[i].rectTransform.localPosition = paths[i].Points.First() ;
            }
        }
    }
}
