using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using verell.Architecture;
using Newtonsoft.Json;

namespace verell.Saves
{
	public sealed class SavesService : SharedObject
	{
		private readonly Dictionary<string, IBaseSaveData> _savesData;
		private readonly ISaveStorage _storage;

		public SavesService()
		{
			_storage = new FileStorage();//Меняем по необходимости на другие варианты сохранения
			_savesData = new Dictionary<string, IBaseSaveData>();
		}

		public async UniTask InitSavesData()
		{
			var savesData = Container.GetAll<IBaseSaveData>().ToList();
			foreach (var saveData in savesData)
			{
				_savesData[saveData.SaveTypeName] = saveData;
			}

			await LoadAllAsync();
		}
		
		private async UniTask LoadAllAsync()
		{
			foreach (var (name, saveData) in _savesData)
			{
				if(saveData == null)
					continue;
				
				var stringValue = await _storage.LoadAsync(name);
				if (string.IsNullOrEmpty(stringValue))
					continue;
				
				JsonConvert.PopulateObject(stringValue, saveData);
			}
		}
		
		public async UniTask SaveAllAsync()
		{
			var savedCount = 0;
			foreach (var (name, saveData) in _savesData)
			{
				await SaveDataAsync(saveData);
				savedCount++;
			}

			await _storage.SaveStorageAsync();
			Debug.Log($"[SavesService] All data has been saved! Count: {savedCount}");
		}

		public async UniTask SaveDataAsync(IBaseSaveData saveData)
		{
			if(saveData == null)
				return;
			
			var stringValue = JsonUtility.ToJson(saveData);
			await _storage.SaveAsync(saveData.SaveTypeName, stringValue);
		}
	}
}
