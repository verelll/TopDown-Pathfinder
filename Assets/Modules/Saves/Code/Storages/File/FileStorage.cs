using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace verell.Saves
{
    public sealed class FileStorage : ISaveStorage
    {
        private readonly string _persistentDataPath;

        public FileStorage()
        {
            _persistentDataPath = Application.persistentDataPath;
        }
		
        async UniTask ISaveStorage.SaveAsync(string dataKey, string value)
        {
            var destination = $"{_persistentDataPath}/{dataKey}.dat";
            var file = File.Exists(destination)
                ? File.OpenWrite(destination)
                : File.Create(destination);
            
            var formatter = new BinaryFormatter();
            formatter.Serialize(file, value);
            file.Close();
        }

        async UniTask ISaveStorage.SaveStorageAsync() { }

        async UniTask<string> ISaveStorage.LoadAsync(string dataKey)
        {
            string destination = $"{_persistentDataPath}/{dataKey}.dat";

            if (!File.Exists(destination))
                return string.Empty;
            
            var file = File.OpenRead(destination);

            var formatter = new BinaryFormatter();
            var data = formatter.Deserialize(file) as string;
            file.Close();
            return data;
        }
    }
}