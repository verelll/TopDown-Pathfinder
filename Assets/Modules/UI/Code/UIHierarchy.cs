using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using verell.ScenesProvider;

namespace verell.UI
{
    public sealed class UIHierarchy : SceneRootObject
    {
        [SerializeField] private Camera _uiCamera;
        [SerializeField] private Canvas _uiCanvas;

        [SerializeField] private Transform _screensLayer;
        
        [SerializeField] private List<UIWidget> _uiWidgets;

        public Camera UICamera => _uiCamera;
        public Canvas UICanvas => _uiCanvas;

        public Transform ScreensLayer => _screensLayer;
        public IReadOnlyList<UIWidget> UIWidgets => _uiWidgets;

#if UNITY_EDITOR
        
        [Button]
        private void Refresh()
        {
            _uiWidgets = new List<UIWidget>();
            _uiWidgets = gameObject.GetComponentsInChildren<UIWidget>().ToList();
        }
        
#endif
    }
}