using System.Collections.Generic;
using verell.Architecture;

namespace verell.RepositoryConfigs
{
	public sealed class ConfigsService : SharedObject
	{
		private readonly RepositoryConfig _repositoryConfig;

		public ConfigsService()
		{
			_repositoryConfig = RepositoryConfig.Instance;
		}

		public T GetConfig<T>() where T : SingleScriptableObject
		{
			return _repositoryConfig.GetConfig<T>();
		}
		
		public IReadOnlyList<T> GetConfigs<T>() where T : MultiScriptableObject
		{
			return _repositoryConfig.GetConfigs<T>();
		}
	}
}

	
