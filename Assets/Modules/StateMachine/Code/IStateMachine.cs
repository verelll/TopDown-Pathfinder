using Cysharp.Threading.Tasks;

namespace verell.StateMachine
{
    public interface IStateMachine
    {
        UniTask Init();
        UniTask Dispose();
        UniTask ChangeState<T>() where T : class, IState;
    }
}