using System.Collections.Generic;

namespace verell.RepositoryConfigs
{
	public static class Configs
	{
		private static RepositoryConfig _repositoryConfig;

		public static T GetConfig<T>() where T : SingleScriptableObject
		{
			_repositoryConfig ??= RepositoryConfig.Instance;
			return _repositoryConfig.GetConfig<T>();
		}
		
		public static IReadOnlyList<T> GetConfigs<T>() where T : MultiScriptableObject
		{
			_repositoryConfig ??= RepositoryConfig.Instance;
			return _repositoryConfig.GetConfigs<T>();
		}
	}
}

	
