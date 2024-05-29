using verell.Architecture;
using verell.StateMachine;

namespace Pathfinder.GameStates
{
    public sealed class GameStateMachine : BaseStateMachine
    {
        public GameStateMachine(IContainer container) 
            : base(container)
        {
            AddState(new LoadingState(this, container));
            AddState(new MenuState(this, container));
            AddState(new GameplayState(this, container));
        }
    }
}