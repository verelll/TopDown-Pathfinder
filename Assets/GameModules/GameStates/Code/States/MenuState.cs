using Cysharp.Threading.Tasks;
using Pathfinder.Menu;
using verell.Architecture;
using verell.StateMachine;

namespace Pathfinder.GameStates
{
    public sealed class MenuState : State
    {
        [Inject] private MenuService _menuService;
        
        public MenuState(IStateMachine stateMachine, IContainer container)
            : base(stateMachine, container) { }
        
        public override async UniTask Enter()
        {
            _menuService.OnPlayClicked += HandlePlayClicked;
            _menuService.OnBackClicked -= HandleBackClicked;
            _menuService.SetMenuActive(true);
        }

        public override async UniTask Exit()
        {
            _menuService.OnPlayClicked -= HandlePlayClicked;
            _menuService.OnBackClicked += HandleBackClicked;
            _menuService.SetMenuActive(false);
        }

        private void HandlePlayClicked()
        {
            Machine.ChangeState<GameplayState>().Forget();
        }
        
        private void HandleBackClicked()
        {
            Machine.ChangeState<MenuState>().Forget();
        }
    }
}