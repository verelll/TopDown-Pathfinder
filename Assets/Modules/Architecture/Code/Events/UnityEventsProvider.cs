using System;
using System.Collections;

namespace verell.Architecture
{
	public sealed class UnityEventsProvider : SingletonMonoBehaviour<UnityEventsProvider>
	{
		public static event Action OnUpdate
		{
			add => Instance._OnUpdate += value;
			remove => Instance._OnUpdate -= value;
		}

		public static event Action OnFixedUpdate
		{
			add => Instance._OnFixedUpdate += value;
			remove => Instance._OnFixedUpdate -= value;
		}

		public static event Action OnLateUpdate
		{
			add => Instance._OnLateUpdate += value;
			remove => Instance._OnLateUpdate -= value;
		}

		public static event Action<bool> OnApplicationFocusChanged
		{
			add => Instance._OnApplicationFocusChanged += value;
			remove => Instance._OnApplicationFocusChanged -= value;
		}

		public static void CoroutineStart(IEnumerator coroutine)
		{
			Instance.StartCoroutine(coroutine);
		}

		public static void CoroutineStop(IEnumerator coroutine)
		{
			Instance.StopCoroutine(coroutine);
		}

		public static void CoroutineStopAll()
		{
			Instance.StopAllCoroutines();
		}

		private event Action _OnUpdate;

		private event Action _OnFixedUpdate;

		private event Action _OnLateUpdate;
		
		private event Action<bool> _OnApplicationFocusChanged;

		private float _nextSecond;

		private void Update()
		{
			_OnUpdate?.Invoke();
		}

		private void FixedUpdate()
		{
			_OnFixedUpdate?.Invoke();
		}

		private void LateUpdate()
		{
			_OnLateUpdate?.Invoke();
		}

		private void OnApplicationFocus(bool hasFocus)
		{
			_OnApplicationFocusChanged?.Invoke(hasFocus);
		}
	}
}
