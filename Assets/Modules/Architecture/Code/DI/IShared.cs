using Cysharp.Threading.Tasks;

namespace verell.Architecture
{
    public interface IShared : ISharedInterface
    {
        UniTask Init();
        UniTask Dispose();
    }
}