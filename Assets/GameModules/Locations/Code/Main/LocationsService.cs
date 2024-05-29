using Cysharp.Threading.Tasks;
using verell.Architecture;
using verell.RepositoryConfigs;
using verell.ScenesProvider;

namespace Pathfinder.Locations
{
	public sealed class LocationsService : SharedObject, ISceneLoader
	{
		public bool IsActive { get; private set; }
		
		private LocationHierarchy _locationHierarchy;
		private LocationsConfig _locationsConfig;
		
		public void EnableLocation()
		{
			_locationHierarchy.gameObject.SetActive(true);
			IsActive = true;
		}
		
		public void DisableLocation()
		{
			_locationHierarchy.gameObject.SetActive(false);
			IsActive = false;
		}

#region ISceneLoader
		
		bool ISceneLoader.NeedLoad => true;
		int ISceneLoader.LoadingPriority => 10;
		string ISceneLoader.LoadingSceneName => _locationsConfig.DefaultLocationName;

		async UniTask ISceneLoader.BeforeLoadingAsync()
		{
			_locationsConfig = Configs.GetConfig<LocationsConfig>();
		}

		void ISceneLoader.InvokeSceneLoaded(SceneRootObject rootObject)
		{
			_locationHierarchy = rootObject as LocationHierarchy;
			DisableLocation();
		}
		
#endregion
	}
}
