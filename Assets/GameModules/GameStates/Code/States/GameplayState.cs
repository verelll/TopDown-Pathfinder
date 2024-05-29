using Cysharp.Threading.Tasks;
using Pathfinder.GameCharacter;
using Pathfinder.Locations;
using verell.Architecture;
using verell.CameraHolder;
using verell.StateMachine;

namespace Pathfinder.GameStates
{
    public sealed class GameplayState : State
    {
        [Inject] private LocationsService _locationsService;
        [Inject] private CameraService _cameraService;
        [Inject] private CharacterCreatorService _characterCreatorService;
        [Inject] private CharacterControllableService _characterControllableService;
        [Inject] private CharacterSaveData _characterSaveData;
        
        public GameplayState(IStateMachine stateMachine, IContainer container)
            : base(stateMachine, container) { }

        public override async UniTask Enter()
        {
            //Location
            _locationsService.EnableLocation();
            
            //Character
            _characterCreatorService.CreateCharacter();
            _characterControllableService.CreateCharacterStateMachine(_characterSaveData, _characterCreatorService.CharacterAgent);
            
            //Camera
            _cameraService.SetCameraActive(true);
            _cameraService.SetCameraTarget(_characterCreatorService.CharacterTransform);
        }

        public override async UniTask Exit()
        {
            //Location
            _locationsService.DisableLocation();
            
            //Camera
            _cameraService.SetCameraActive(false);
            _cameraService.SetCameraTarget(default);
        }
    }
}