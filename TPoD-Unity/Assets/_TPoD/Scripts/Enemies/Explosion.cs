using Metamesa.MMUnity.ObjectPooling;
using System.Collections;
using UnityEngine;

namespace TPoD
{
	public class Explosion : MonoBehaviour, IPoolable
	{
		private void OnEnable()
		{
			StartCoroutine(DoTheBoom());
		}

		private IEnumerator DoTheBoom()
		{
			float startTime = Time.time;
			while (Time.time - startTime < 1)
			{
				yield return null;
			}

			ReturnSelfToObjectPool();
		}

		#region IPoolable

		private ObjectPool<Explosion> _objectPool;
		public void ActivatePoolable<T>(ObjectPool<T> objectPool) where T : IPoolable
		{
			_objectPool = objectPool as ObjectPool<Explosion>;
			this.gameObject.SetActive(true);
		}

		public void DeactivatePoolable()
		{
			this.gameObject.SetActive(false);
		}

		public void ReturnSelfToObjectPool()
		{
			if (_objectPool != null)
				_objectPool.ReturnObjectToPool(this);
			else
				Destroy(this.gameObject);
		}

		#endregion
	}
}
