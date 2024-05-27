using System.Collections.Generic;
using UnityEngine;
using verell.RepositoryConfigs;

namespace verell.UI
{
    [CreateAssetMenu(
        menuName = "verell/UI/" + nameof(UIConfig), 
        fileName = nameof(UIConfig))]
    public sealed class UIConfig : SingleScriptableObject
    {
        [SerializeField] private Dictionary<UIScreenType, UIScreenPair> _screens;

        internal IReadOnlyDictionary<UIScreenType, UIScreenPair> Screens => _screens;
    }

    internal sealed class UIScreenPair
    {
        [SerializeField] private UIScreen _screenPrefab;
        [SerializeField] private bool _preload;
        [SerializeField] private bool _showByDefault;

        public UIScreen ScreenPrefab => _screenPrefab;
        public bool Preload => _preload;
        public bool ShowByDefault => _showByDefault;
    }
}