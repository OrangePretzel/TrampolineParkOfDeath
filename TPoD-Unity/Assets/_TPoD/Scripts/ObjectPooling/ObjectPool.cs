using System;
using System.Collections.Generic;
using UnityEngine;

namespace Metamesa.MMUnity.ObjectPooling
{
	public abstract class ObjectPool<T> : MonoBehaviour where T : IPoolable
	{
		[SerializeField] private GameObject _poolablePrefab;

		private Queue<T> _poolables = new Queue<T>();

		public void PreallocateObjects(int count)
		{
			for (int i = 0; i < count; i++)
				ReturnObjectToPool(CreatePoolable());
		}

		public T GetObjectFromPool()
		{
			if (_poolables.Count < 1)
			{
				PreallocateObjects(1);
			}

#if UNITY_EDITOR
			// Editor Only: Safety check to ensure allocation was successful
			if (_poolables.Count < 1)
				throw new Exception($"Allocation Function failed to allocate poolables of type [{typeof(T).FullName}]");
#endif

			T poolable = _poolables.Dequeue();
			poolable.ActivatePoolable(this);

			return poolable;
		}

		public void ReturnObjectToPool(T poolable)
		{
			if (poolable == null)
				return; // Object is likely already destroyed. Ignore it

			// Deactivate the poolable
			poolable.DeactivatePoolable();

			// Add it to the list
			_poolables.Enqueue(poolable);
		}

		private T CreatePoolable()
		{
			var p = Instantiate(_poolablePrefab) as GameObject;
			var component = p.GetComponent<T>();

			if (component == null)
				throw new Exception($"Component of type {typeof(T).FullName} was not found on poolable prefab");

			return component;
		}
	}
}
