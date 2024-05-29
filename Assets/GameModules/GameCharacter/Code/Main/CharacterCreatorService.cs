using UnityEngine;
using UnityEngine.AI;
using verell.Architecture;
using verell.RepositoryConfigs;
using Object = UnityEngine.Object;

namespace Pathfinder.GameCharacter
{
	public sealed class CharacterCreatorService : SharedObject
	{
		public Transform CharacterTransform => _characterBehaviour.transform;
		public NavMeshAgent CharacterAgent => _characterBehaviour.Agent;

		private readonly CharacterConfig _characterConfig;
		private CharacterBehaviour _characterBehaviour;

		public CharacterCreatorService()
		{
			_characterConfig = Configs.GetConfig<CharacterConfig>();
		}

		public void CreateCharacter()
		{
			if(_characterBehaviour != null)
				return;
			
			_characterBehaviour = Object.Instantiate(_characterConfig.MainCharacterPrefab);
			_characterBehaviour.Agent.speed = _characterConfig.MoveSpeed;
			_characterBehaviour.Agent.angularSpeed = _characterConfig.RotationSpeed;
		}
	}
}
