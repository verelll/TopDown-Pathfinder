using Cysharp.Threading.Tasks;
using UnityEngine;
using verell.Architecture;
using verell.RepositoryConfigs;

namespace verell.CameraHolder
{
    public sealed class CameraService : SharedObject
    {
        private const float MinBorder = 0.2f;
        private const float MaxBorder = 0.8f;
        
        public Camera GeneralCamera { get; private set; }
        public bool IsActive { get; private set; }

        private readonly CameraConfig _cameraConfig;
        
        private CameraHierarchy _cameraHierarchy;
        private Transform _cameraTarget;
        private Vector3 _cameraVelocity;
        
        public CameraService()
        {
            _cameraConfig = Configs.GetConfig<CameraConfig>();
        }

        protected override async UniTask Init()
        {
            CreateCameraHolder();
            SetCameraActive(false);
            
            UnityEventsProvider.OnUpdate += HandleUpdate;
        }

        protected override async UniTask Dispose()
        {
            UnityEventsProvider.OnUpdate -= HandleUpdate;
        }

        private void CreateCameraHolder()
        {
            _cameraHierarchy = Object.Instantiate(_cameraConfig.CameraPrefab);
            GeneralCamera = _cameraHierarchy.GeneralCamera;
        }

        public void SetCameraTarget(Transform target)
        {
            _cameraTarget = target;
        }
        
        public void SetCameraActive(bool active)
        {
            IsActive = active;
            _cameraHierarchy.gameObject.SetActive(IsActive);
        }

        private void HandleUpdate()
        {
            if(!IsActive || _cameraTarget == null || IsInViewport())
                return;
            
            var cameraPos = _cameraHierarchy.transform.position;
            var newPos = Vector3.SmoothDamp(cameraPos, _cameraTarget.position, ref _cameraVelocity, _cameraConfig.FollowedSpeed);
            _cameraHierarchy.transform.position = newPos;
        }

        private bool IsInViewport()
        {
            if (_cameraTarget == null)
                return default;
            
            var viewportPoint = _cameraHierarchy.GeneralCamera.WorldToViewportPoint(_cameraTarget.position);
            return viewportPoint.x >= MinBorder 
                   && viewportPoint.x <= MaxBorder 
                   && viewportPoint.y >= MinBorder
                   && viewportPoint.y <= MaxBorder
                   && viewportPoint.z > MinBorder;
        }
    }
}