using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace verell.UI
{
    //Одновременно может быть активен только один экран
    [RequireComponent(typeof(Canvas))]
    [RequireComponent(typeof(GraphicRaycaster))]
    public abstract class UIScreen : BaseUIElement
    {
        [FoldoutGroup("Screen Settings"), SerializeField]
        private GameObject _rootGO;
        
        [FoldoutGroup("Screen Settings"), SerializeField]
        private Canvas _canvas;
        
        public abstract UIScreenType ScreenType { get; }
        
        public bool IsActive { get; private set; }

        public event Action OnScreenShowed;
        public event Action OnScreenHided;

        internal void Show()
        {
            _rootGO.SetActive(true);
            OnShow();
            OnScreenShowed?.Invoke();
        }
        
        protected virtual void OnShow() { }
        
        internal void Hide()
        {
            _rootGO.SetActive(false);
            OnHide();
            OnScreenHided?.Invoke();
        }
        
        protected virtual void OnHide() { }
    }

    public enum UIScreenType
    {
        Menu = 10,
    }
}