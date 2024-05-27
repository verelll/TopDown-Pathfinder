using Cysharp.Threading.Tasks;

namespace verell.StateMachine
{
    public interface IState
    {
        UniTask Enter();
        void Update();
        UniTask Exit();
    }
}