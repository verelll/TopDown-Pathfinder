using Cysharp.Threading.Tasks;

namespace verell.StateMachine
{
    public interface IState
    {
        UniTask Enter();
        UniTask Exit();
    }
}