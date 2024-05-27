using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using verell.Architecture;
using verell.RepositoryConfigs;
using verell.ScenesProvider;
using Object = UnityEngine.Object;

namespace verell.UI
{
	public sealed class UIService : SharedObject, ISceneLoader
	{
		private const string UISceneName = "UIScene";

		[Inject] private ConfigsService _configsService;
		
		private readonly List<IUIElement> _uiElements;
		
		private UIHierarchy _uiHierarchy;
		private UIConfig _config;

		public UIService()
		{
			_uiElements = new List<IUIElement>();

			_widgets = new Dictionary<UIWidgetType, UIWidget>();
			_widgetsByType = new Dictionary<Type, UIWidget>();
			
			_screens = new Dictionary<UIScreenType, UIScreen>();
			_screensByType = new Dictionary<Type, UIScreen>();
		}

		protected override async UniTask Init()
		{
			_config = _configsService.GetConfig<UIConfig>();

			InitWidgets();
			CreateScreens();
		}

		protected override async UniTask Dispose()
		{
			foreach (var uiElement in _uiElements)
			{
				DisposeUIElement(uiElement);
			}
		}

#region Main

		private void InitUIElement(IUIElement element)
		{
			Container.InjectAt(element);
			element.Init();
			_uiElements.Add(element);
		}

		private void DisposeUIElement(IUIElement element)
		{
			element.Dispose();
			_uiElements.Remove(element);
		}
		

#endregion

		
#region Widgets

		private readonly Dictionary<UIWidgetType, UIWidget> _widgets;
		private readonly Dictionary<Type, UIWidget> _widgetsByType;

		private void InitWidgets()
		{
			foreach (var widget in _uiHierarchy.UIWidgets)
			{
				InitUIElement(widget);
				_widgets[widget.WidgetType] = widget;
				_widgetsByType[widget.GetType()] = widget;
			}
		}
		
		public T GetWidget<T>() where T : UIWidget
		{
			var type = typeof(T);
			if (!_widgetsByType.TryGetValue(type, out var widget))
				return default;

			return (T)widget;
		}
        
		public UIWidget GetWidget(UIWidgetType type)
		{
			if (!_widgets.TryGetValue(type, out var widget))
				return default;

			return widget;
		}

		public void SetWidgetActive(UIWidgetType type, bool active)
		{
			var widget = GetWidget(type);
			widget.gameObject.SetActive(active);
		}

#endregion

		
#region Screens
        
        internal IReadOnlyDictionary<UIScreenType, UIScreen> AllScreens => _screens;

        private readonly Dictionary<UIScreenType, UIScreen> _screens;
        private readonly Dictionary<Type, UIScreen> _screensByType;

        private UIScreen _activeScreen;
        
        public event Action<UIScreen> OnScreenShowed;
        public event Action<UIScreen> OnScreenHided;

        public void ChangeActiveScreen(UIScreenType type)
        {
	        if(_activeScreen != null)
	        {
		        HideScreen(_activeScreen.ScreenType);
	        }
	        
	        _activeScreen = GetOrCreateScreen(type);
	        _activeScreen.Show();
	        OnScreenShowed?.Invoke(_activeScreen);
        }

        public void HideScreen(UIScreenType type)
        {
	        var screen = GetOrCreateScreen(type);
	        screen.Hide();
	        OnScreenHided?.Invoke(screen);
        }

        public T GetScreen<T>() where T : UIScreen
        {
            var type = typeof(T);
            if (!_screensByType.TryGetValue(type, out var screen))
                return default;

            return (T)screen;
        }
        
        public UIScreen GetScreen(UIScreenType type)
        {
            if (!_screens.TryGetValue(type, out var screen))
                return default;

            return screen;
        }

        public bool IsScreenActive(UIScreenType type)
        {
            var screen = GetScreen(type);
            if (screen == null)
                return false;

            return screen.IsActive;
        }
        
        private void CreateScreens()
        {
	        foreach (var pair in _config.Screens)
	        {
		        if(!pair.Value.Preload 
		           || pair.Value.ScreenPrefab == null)
			        continue;
		        
		        var screen = GetOrCreateScreen(pair.Key);
		        if(!pair.Value.ShowByDefault)
					screen.Hide();
	        }
        }
        
        private UIScreen GetOrCreateScreen(UIScreenType type)
        {
	        if (_screens.TryGetValue(type, out var screen))
		        return screen;
                    
	        if(!_config.Screens.TryGetValue(type, out var screenSettings))
		        return default;
                    
	        var prefab = screenSettings.ScreenPrefab;
	        screen = Object.Instantiate(prefab, _uiHierarchy.ScreensLayer);
	        InitUIElement(screen);
	        _screens[type] = screen;
	        _screensByType[prefab.GetType()] = screen;
	        return screen;
        }

#endregion
		

#region ISceneLoader
		
		bool ISceneLoader.NeedLoad => true;
		int ISceneLoader.LoadingPriority => 10;
		string ISceneLoader.LoadingSceneName => UISceneName;
		async UniTask ISceneLoader.BeforeLoadingAsync() { }

		void ISceneLoader.InvokeSceneLoaded(SceneRootObject rootObject)
		{
			_uiHierarchy = rootObject as UIHierarchy;
		}
		
#endregion
		
	}
}
