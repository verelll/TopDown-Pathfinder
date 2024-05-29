using System;
using verell.Architecture;
using verell.UI;

namespace Pathfinder.Menu
{
    public sealed class MenuService : SharedObject
    {
        [Inject] private UIService _uiService;
        
        public event Action OnPlayClicked;
        public event Action OnBackClicked;

        public void SetMenuActive(bool active)
        {
            if(active)
            {
                _uiService.ChangeActiveScreen(UIScreenType.Menu);
            }
            else
            {
                _uiService.HideScreen(UIScreenType.Menu);
            }
            
            _uiService.SetWidgetActive(UIWidgetType.MenuBack, !active);
        }
        
        internal void InvokePlayClicked()
        {
            OnPlayClicked?.Invoke();
        }

        internal void InvokeBackClicked()
        {
            OnBackClicked?.Invoke();
        }
    }
}