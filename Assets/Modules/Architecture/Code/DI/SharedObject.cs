using Cysharp.Threading.Tasks;

namespace verell.Architecture
{
    public abstract class SharedObject : IShared
    {
        protected IContainer Container { get; private set; }

        void ISharedInterface.SetContainer(IContainer container) => Container = container;

        async UniTask IShared.Init() => await Init();
        async UniTask IShared.Dispose() => await Dispose();

        protected virtual async UniTask Init() { }
        protected virtual async UniTask Dispose() { }
    }
}