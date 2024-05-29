using Sirenix.OdinInspector;
using UnityEngine;
using verell.RepositoryConfigs;

namespace Pathfinder.GameCharacter
{
    [CreateAssetMenu(
        menuName = "verell/Characters/" + nameof(CharacterConfig), 
        fileName = nameof(CharacterConfig))]
    public sealed class CharacterConfig : SingleScriptableObject
    {
        [SerializeField, BoxGroup("Main Settings")] 
        private CharacterBehaviour _mainCharacterPrefab;

        [SerializeField, BoxGroup("Main Settings")] 
        private float _moveSpeed;

        [SerializeField, BoxGroup("Main Settings")] 
        private float _rotationSpeed;

        [SerializeField, BoxGroup("States Settings")] 
        private int _idleStateDuration;
        
        [SerializeField, BoxGroup("States Settings")] 
        private int _maxPointsCount;
        
        public CharacterBehaviour MainCharacterPrefab => _mainCharacterPrefab;
        public float MoveSpeed => _moveSpeed;
        public float RotationSpeed => _rotationSpeed;
        public int IdleStateDuration => _idleStateDuration;
        
        public int MaxPointsCount => _maxPointsCount;
    }
}