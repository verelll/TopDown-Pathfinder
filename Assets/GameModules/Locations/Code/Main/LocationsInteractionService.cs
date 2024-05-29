using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using verell.Architecture;
using verell.CameraHolder;
using verell.Inputs;

namespace Pathfinder.Locations
{
    public sealed class LocationsInteractionService : SharedObject
    {
        [Inject] private InputService _inputService;
        [Inject] private CameraService _cameraService;

        public event Action<BaseLocationObject, Vector3> OnClicked;

        protected override async UniTask Init()
        {
            _inputService.OnScreenClicked += HandleScreenClicked;
        }

        protected override async UniTask Dispose()
        {
            _inputService.OnScreenClicked -= HandleScreenClicked;
        }

        private void HandleScreenClicked(Vector2 clickScreenPos)
        {
            var ray = _cameraService.GeneralCamera.ScreenPointToRay(clickScreenPos);
            if (!Physics.Raycast(ray, out var hit)) 
                return;

            var clickPos = hit.point;
            var targetObject = hit.collider.GetComponent<BaseLocationObject>() 
                               ?? hit.collider.GetComponentInParent<BaseLocationObject>();
			
            OnClicked?.Invoke(targetObject, clickPos);
        }
    }
}