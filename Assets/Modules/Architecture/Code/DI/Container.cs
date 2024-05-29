using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Cysharp.Threading.Tasks;

namespace verell.Architecture
{
	public sealed class Container : IContainer
	{
		private readonly Dictionary<Type, IShared> _shareds;

		public string Name   { get; }
		public bool IsActive { get; private set; }

		public Container(string name)
		{
			Name = name;
			
			IsActive   = false;
			_shareds = new Dictionary<Type, IShared>();
		}

		public async UniTask Init(Action<IShared> sharedInitialized = null)
		{
			foreach (var shared in _shareds.Values)
			{
				await shared.Init();
				sharedInitialized?.Invoke(shared);
			}

			IsActive = true;
		}

		public async UniTask Dispose(Action<IShared> sharedDisposed = null)
		{
			IsActive = false;

			foreach (var shared in _shareds.Values)
			{
				await shared.Dispose();
				sharedDisposed?.Invoke(shared);
			}
		}
		
		public void ApplyDependencies()
		{
			foreach (var (type, sharedObject) in _shareds)
			{
				sharedObject.SetContainer(this);
				SetFields(type, sharedObject);
			}
		}

		public T Add<T>() where T : IShared, new()
		{
			return Add(new T());
		}
		
		public T Add<T>(T shared) where T : IShared
		{
			var type = typeof(T);
			if (_shareds.ContainsKey(type))
				throw new Exception($"[Architecture] Container already contains instance of Type '{type.Name}'");
			_shareds.Add(type, shared);
			
			var inter = typeof(ISharedInterface);
			var subtypes = @type.GetInterfaces();
			foreach (var sub in subtypes)
			{
				if (sub == inter || sub == typeof(IShared))
					continue;
				
				if (!inter.IsAssignableFrom(sub))
					continue;
				
				if (_shareds.ContainsKey(sub))
					throw new Exception($"[Architecture] Container already contains instance of Interface '{sub.Name}'");
				_shareds.Add(sub, shared);
			}
			
			return shared;
		}

		private object Get(Type type)
		{
			if (!_shareds.TryGetValue(type, out var shared))
				throw new Exception($"[Architecture] Container doesn't contains element of Type '{type.Name}'");
			
			return shared;
		}

		private void SetFields(Type type, object target)
		{
			var containerType = typeof(Container);
			var fields = type.GetAllFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			foreach (var field in fields)
			{
				var attr = Attribute.GetCustomAttribute(field, typeof(Inject));
				if (attr == null) 
					continue;
					
				var other = (field.FieldType == containerType) 
					? this 
					: Get(field.FieldType);
					
				field.SetValue(target, other);
			}
		}
		
		void IContainer.InjectAt(object target)
		{
			var type          = target.GetType();
			SetFields(type, target);
		}

		T IContainer.Get<T>()
		{
			var type = typeof(T);
			if (!_shareds.TryGetValue(type, out var shared))
				throw new Exception($"[Architecture] Container doesn't contains element of Type '{type.Name}'");
			
			return (T) shared;
		}

		object IContainer.Get(Type type)
		{
			return Get(type);
		}

		IReadOnlyList<T> IContainer.GetAll<T>()
		{
			var target = typeof(T);
			var objects = new List<T>();
			
			foreach (var shared in _shareds.Values)
			{
				if (shared.GetType().GetInterfaces().Contains(target))
					objects.Add((T) shared);
			}

			return objects;
		}
	}
}