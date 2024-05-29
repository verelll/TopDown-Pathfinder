using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using verell.Architecture;
using verell.UI;

namespace Pathfinder.Menu
{
    public sealed class MenuBackWidget : UIWidget
    {
        [Inject] private MenuService _menuService;
		
        [BoxGroup("Menu Back Settings"), SerializeField]
        private Button _backButton;
		
        public override UIWidgetType WidgetType => UIWidgetType.MenuBack;

        protected override void Init()
        {
            _backButton.onClick.AddListener(HandleBackClick);
        }

        protected override void Dispose()
        {
            _backButton.onClick.RemoveListener(HandleBackClick);
        }
		
        private void HandleBackClick()
        {
            _menuService.InvokeBackClicked();
        }
    }
}