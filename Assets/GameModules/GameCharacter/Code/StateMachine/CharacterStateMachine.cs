using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Pathfinder.Locations;
using UnityEngine;
using UnityEngine.AI;
using verell.Architecture;
using verell.StateMachine;

namespace Pathfinder.GameCharacter
{
    public sealed class CharacterStateMachine : BaseStateMachine
    {
        [Inject] private LocationsInteractionService _locationsInteractionService;
        
        private readonly Queue<Vector3> _movePoints;
        private readonly ICharacterSaveData _characterSaveData;
        private readonly CharacterConfig _characterConfig;
        
        public CharacterStateMachine(
            IContainer container, 
            CharacterConfig config, 
            ICharacterSaveData characterSaveData, 
            NavMeshAgent characterAgent) 
            : base(container)
        {
            _movePoints = new Queue<Vector3>();
            _characterSaveData = characterSaveData;
            _characterConfig = config;
            
            AddState(new CharacterIdleState(this, container, _characterConfig));
            AddState(new CharacterMoveState(this, container, characterAgent));
        }

        public override async UniTask Init()
        {
            LoadPoints();
            ChangeState<CharacterMoveState>();
            _locationsInteractionService.OnClicked += HandleClicked;
        }

        public override async UniTask Dispose()
        {
            SavePoints();
            ActiveState = null;
            _locationsInteractionService.OnClicked -= HandleClicked;
        }
        
        private void HandleClicked(BaseLocationObject clickedObject, Vector3 clickPos)
        {
            if(_movePoints.Count >= _characterConfig.MaxPointsCount)
            {
                Debug.Log($"Maximum number of points in the queue has been reached. Max: {_characterConfig.MaxPointsCount}");
                return;
            }
            
            var isObstacle = clickedObject is LocationObstacle;
            if(isObstacle)
                return;

            _movePoints.Enqueue(clickPos);
        }

        internal bool TryGetNextPoint(out Vector3 movePoint)
        {
            return _movePoints.TryPeek(out movePoint);
        }

        internal void RemoveActiveTarget()
        {
            if(_movePoints.Count == 0)
                return;
            
            _movePoints.Dequeue();
        }

#region Save / Load
        
        private void LoadPoints()
        {
            if (!_characterSaveData.TryGetMovePoints(out var points))
                return;
         
            foreach (var point in points)
            {
                _movePoints.Enqueue(point);
            }
        }
        
        private void SavePoints()
        {
            _characterSaveData.SetMovePoints(_movePoints.ToList());
        }
        
#endregion
        
    }
}