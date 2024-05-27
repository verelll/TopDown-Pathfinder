using Cysharp.Threading.Tasks;

namespace verell.Saves
{
    public sealed class RemoteStorage : ISaveStorage
    {
        async UniTask ISaveStorage.SaveAsync(string dataKey, string value)
        {
            //Сохранение на облако
        }

        async UniTask ISaveStorage.SaveStorageAsync()
        {
            //Выгрузка на сервер (по необходимости)
        }

        async UniTask<string> ISaveStorage.LoadAsync(string dataKey)
        {
            //загрузка с облака
            return string.Empty;
        }
    }
}