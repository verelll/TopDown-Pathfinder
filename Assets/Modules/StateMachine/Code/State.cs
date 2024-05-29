using Cysharp.Threading.Tasks;
using verell.Architecture;

namespace verell.StateMachine
{
    public abstract class State : IState
    {
        protected IStateMachine Machine { get; }
        protected IContainer Container { get; }

        protected State(IStateMachine stateMachine, IContainer container)
        {
            Machine = stateMachine;
            Container = container;
        }

        public abstract UniTask Enter();
        public abstract UniTask Exit();
    }
}