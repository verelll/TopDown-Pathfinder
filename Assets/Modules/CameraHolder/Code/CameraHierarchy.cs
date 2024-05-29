using Sirenix.OdinInspector;
using UnityEngine;

namespace verell.CameraHolder
{
	public sealed class CameraHierarchy : SerializedMonoBehaviour
	{
		[SerializeField] private Camera _camera;
		public Camera GeneralCamera => _camera;
	}
}
