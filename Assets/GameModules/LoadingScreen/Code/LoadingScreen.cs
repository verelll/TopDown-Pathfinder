using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Pathfinder.ScreenLoading
{
    public sealed class LoadingScreen : SerializedMonoBehaviour
    {
        [SerializeField, BoxGroup("Main Settings")] 
        private CanvasGroup _canvasGroup;
        
        [SerializeField, BoxGroup("Animation Settings")]
        private float _fadeDuration;
        
        internal async UniTask ShowScreenAsync()
        {
            gameObject.SetActive(true);

            var showed = false;
            var sequence = DOTween.Sequence();
            sequence.Append(_canvasGroup.DOFade(1, _fadeDuration));
            sequence.OnComplete(() => showed = true);
            
            await UniTask.WaitUntil(() => showed);
        }

        internal async UniTask HideScreenAsync()
        {
            var hided = false;
            var sequence = DOTween.Sequence();
            sequence.Append(_canvasGroup.DOFade(0, _fadeDuration));
            sequence.OnComplete(() => hided = true);
            
            await UniTask.WaitUntil(() => hided);
            gameObject.SetActive(false);
        }
    }
}