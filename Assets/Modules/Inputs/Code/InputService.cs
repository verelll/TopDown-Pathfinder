using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using verell.Architecture;

namespace verell.Inputs
{
	public sealed class InputService : SharedObject
	{
		private const int LeftMouseButtonIndex = 0;
		private const int FirstTouchIndex = 0;
		
		public event Action<Vector2> OnScreenClicked;
		
		protected override async UniTask Init()
		{
			UnityEventsProvider.OnUpdate += HandleUpdate;
		}

		protected override async UniTask Dispose()
		{
			UnityEventsProvider.OnUpdate -= HandleUpdate;
		}

		private void HandleUpdate()
		{
			UpdateMouse();
			UpdateTouch();
		}

		private void UpdateMouse()
		{
			if (!Input.GetMouseButtonDown(LeftMouseButtonIndex))
				return;

			Click(Input.mousePosition);
		}

		private void UpdateTouch()
		{
			if (Input.touchCount <= 0) 
				return;
			
			var touch = Input.GetTouch(FirstTouchIndex);
			if (touch.phase == TouchPhase.Began)
				Click(touch.position);
		}

		private void Click(Vector2 screenClickPos)
		{
			OnScreenClicked?.Invoke(screenClickPos);
		}
	}
}
