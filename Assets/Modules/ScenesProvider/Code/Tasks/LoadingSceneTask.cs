using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using verell.Architecture;

namespace verell.ScenesProvider
{
    public sealed class LoadingSceneTask : ITask
    {
        private AsyncOperation _loadingOperation;
        private readonly ISceneLoader _loader;

        public LoadingSceneTask(ISceneLoader loader)
        {
            _loader = loader;
        }

        public async UniTask StartAsync()
        {
            await _loader.BeforeLoadingAsync();
            if (!_loader.NeedLoad)
                return;
			
            _loadingOperation = SceneManager.LoadSceneAsync(_loader.LoadingSceneName, LoadSceneMode.Additive);
            _loadingOperation.priority = _loader.LoadingPriority;
            _loadingOperation.allowSceneActivation = true;
            await _loadingOperation;
			
            var hierarchy = Object
                .FindObjectsOfType<SceneRootObject>()
                .FirstOrDefault(o => o.gameObject.scene.name == _loader.LoadingSceneName);
			
            _loader.InvokeSceneLoaded(hierarchy);
            hierarchy?.Init();
        }
    }
}