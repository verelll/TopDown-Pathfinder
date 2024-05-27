using Cysharp.Threading.Tasks;

namespace verell.StateMachine
{
    public interface IStateMachine
    {
        void Update();

        UniTask ChangeState<T>() where T : class, IState;
    }
}