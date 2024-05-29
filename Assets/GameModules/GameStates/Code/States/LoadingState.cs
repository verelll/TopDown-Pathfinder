using Cysharp.Threading.Tasks;
using Pathfinder.ScreenLoading;
using UnityEngine;
using verell.Architecture;
using verell.Saves;
using verell.ScenesProvider;
using verell.StateMachine;

namespace Pathfinder.GameStates
{
    public sealed class LoadingState : State
    {
        private const int FakeLoadingMilliseconds = 2000;
        
        [Inject] private LoadingScreenService _loadingScreenService;
        [Inject] private SavesService _savesService;

        public LoadingState(IStateMachine stateMachine, IContainer container)
            : base(stateMachine, container) { }
        
        public override async UniTask Enter()
        { 
            await _loadingScreenService.ShowScreenAsync();
            await LoadScenesAsync();
            await _savesService.InitSavesData();
        }

        public override async UniTask Exit()
        {
            await UniTask.Delay(FakeLoadingMilliseconds);
            await _loadingScreenService.HideScreenAsync();
        }

        private async UniTask LoadScenesAsync()
        {
            var loaders = Container.GetAll<ISceneLoader>();
            var tasksQueue = new TasksQueue();
            foreach (var sceneLoader in loaders)
            {
                tasksQueue.AddTask(new LoadingSceneTask(sceneLoader));
            }
            
            await tasksQueue.StartAsync();
        }
    }
}