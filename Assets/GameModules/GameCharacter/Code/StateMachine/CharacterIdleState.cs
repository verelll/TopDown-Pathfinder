using Cysharp.Threading.Tasks;
using verell.Architecture;
using verell.StateMachine;

namespace Pathfinder.GameCharacter
{
    public sealed class CharacterIdleState : State
    {
        private const int MillisecondsInSec = 1000;
        
        private readonly CharacterConfig _characterConfig;

        public CharacterIdleState(
            IStateMachine stateMachine, 
            IContainer container, 
            CharacterConfig config)
            : base(stateMachine, container)
        {
            _characterConfig = config;
        }

        public override async UniTask Enter()
        {
            await UniTask.Delay(_characterConfig.IdleStateDuration * MillisecondsInSec);
            Machine.ChangeState<CharacterMoveState>();
        }

        public override async UniTask Exit() { }
    }
}