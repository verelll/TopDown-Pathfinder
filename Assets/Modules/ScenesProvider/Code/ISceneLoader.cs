using Cysharp.Threading.Tasks;

namespace verell.ScenesProvider
{
    public interface ISceneLoader
    {
        bool NeedLoad { get; }
        int LoadingPriority { get; }
        string LoadingSceneName { get; }
        UniTask BeforeLoadingAsync();
        void InvokeSceneLoaded(SceneRootObject rootObject);
    }
}