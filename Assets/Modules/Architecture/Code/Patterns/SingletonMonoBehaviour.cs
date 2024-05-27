using UnityEngine;
using Sirenix.OdinInspector;

namespace verell.Architecture
{
	public abstract class SingletonMonoBehaviour<T> : SerializedMonoBehaviour where T : SingletonMonoBehaviour<T>
	{
		private static T _instance;

		public static T Instance
		{
			get
			{
				if (_instance != null) 
					return _instance;
				
				var o = new GameObject(typeof(T).Name);
				_instance = o.AddComponent<T>();
				DontDestroyOnLoad(o);
				return _instance;
			}
		}
	}
}