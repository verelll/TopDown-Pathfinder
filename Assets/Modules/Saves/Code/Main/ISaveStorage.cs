using Cysharp.Threading.Tasks;

namespace verell.Saves
{
    public interface ISaveStorage
    {
        UniTask SaveAsync(string dataKey, string value);
        UniTask SaveStorageAsync(); // Используется для выгрузки на облако
        UniTask<string> LoadAsync(string dataKey);
    }
}