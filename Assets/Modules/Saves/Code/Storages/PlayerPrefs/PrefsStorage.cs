using Cysharp.Threading.Tasks;
using UnityEngine;

namespace verell.Saves
{
    public sealed class PrefsStorage : ISaveStorage
    {
        async UniTask ISaveStorage.SaveAsync(string dataKey, string value)
        {
            PlayerPrefs.SetString(dataKey, value);
        }

        async UniTask ISaveStorage.SaveStorageAsync()
        {
            PlayerPrefs.Save();
        }

        async UniTask<string> ISaveStorage.LoadAsync(string dataKey)
        {
            return PlayerPrefs.GetString(dataKey, string.Empty);
        }
    }
}