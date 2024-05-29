using UnityEngine;
using verell.RepositoryConfigs;

namespace verell.CameraHolder
{
    [CreateAssetMenu(
        menuName = "verell/Camera/" + nameof(CameraConfig), 
        fileName = nameof(CameraConfig))]
    public sealed class CameraConfig : SingleScriptableObject
    {
        [SerializeField] 
        private CameraHierarchy _cameraPrefab;

        [SerializeField] 
        private float _followedSpeed;

        public CameraHierarchy CameraPrefab => _cameraPrefab;
        public float FollowedSpeed => _followedSpeed;
    }
}