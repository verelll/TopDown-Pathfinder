using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using verell.Architecture;
using verell.StateMachine;

namespace Pathfinder.GameCharacter
{
    public sealed class CharacterMoveState : State
    {
        private readonly CharacterStateMachine _characterStateMachine;
        private readonly NavMeshAgent _characterAgent;
        
        public Vector3? TargetPoint { get; private set; }
        
        public CharacterMoveState(
            IStateMachine stateMachine, 
            IContainer container, 
            NavMeshAgent characterAgent)
            : base(stateMachine, container)
        {
            TargetPoint = default;
            _characterStateMachine = stateMachine as CharacterStateMachine;
            _characterAgent = characterAgent;
        }

        public override async UniTask Enter()
        {
            TargetPoint = default;
            if (!_characterStateMachine.TryGetNextPoint(out var targetPoint))
            {
                Machine.ChangeState<CharacterIdleState>();
                return;
            }

            TargetPoint = targetPoint;
            _characterAgent.SetDestination(TargetPoint.Value);
            await ExitFromState();
        }

        private async UniTask ExitFromState()
        {
            await UniTask.WaitWhile(() => _characterAgent != null && _characterAgent.remainingDistance > 0.1f);
            TargetPoint = default;
            Machine.ChangeState<CharacterIdleState>();
        }

        public override async UniTask Exit()
        {
            _characterStateMachine.RemoveActiveTarget();
        }
    }
}