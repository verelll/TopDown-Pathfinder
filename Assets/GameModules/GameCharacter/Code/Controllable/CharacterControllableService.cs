using Cysharp.Threading.Tasks;
using UnityEngine.AI;
using verell.Architecture;
using verell.RepositoryConfigs;
using verell.StateMachine;

namespace Pathfinder.GameCharacter
{
    public sealed class CharacterControllableService : SharedObject
    {
        private readonly CharacterConfig _characterConfig;
        private IStateMachine _characterStateMachine;

        public CharacterControllableService()
        {
            _characterConfig = Configs.GetConfig<CharacterConfig>();
        }

        protected override async UniTask Dispose()
        {
            _characterStateMachine?.Dispose();
        }

        public void CreateCharacterStateMachine(ICharacterSaveData characterSaveData, NavMeshAgent characterAgent)
        {
            _characterStateMachine?.Dispose();
            
            _characterStateMachine = new CharacterStateMachine(Container, _characterConfig, characterSaveData, characterAgent);
            Container.InjectAt(_characterStateMachine);
            _characterStateMachine.Init();
        }
    }
}