using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using verell.Architecture;
using verell.UI;

namespace Pathfinder.Menu
{
	public sealed class MenuScreen : UIScreen
	{
		[Inject] private MenuService _menuService;
		
		[BoxGroup("Menu Settings"), SerializeField]
		private Button _playButton;
		
		public override UIScreenType ScreenType => UIScreenType.Menu;

		protected override void Init()
		{
			_playButton.onClick.AddListener(HandlePlayClick);
		}

		protected override void Dispose()
		{
			_playButton.onClick.RemoveListener(HandlePlayClick);
		}

		private void HandlePlayClick()
		{
			_menuService.InvokePlayClicked();
		}
	}
}
