using Metamesa.MMUnity.ObjectPooling;
using System;
using System.Collections;
using UnityEngine;

namespace TPoD
{
	public class DyingWasp : MonoBehaviour, IPoolable
	{
		[SerializeField] private WaspEnemyAnimator _animator = null;
		[SerializeField] private Rigidbody _rigidbody = null;

		[SerializeField] private float _dyingVelocity = 30;

		private void OnEnable()
		{
			transform.localScale = Vector3.one;
			_rigidbody.velocity = Vector3.zero;
			transform.localRotation = Quaternion.Euler(0, 0, 0);
			StartCoroutine(DoTheBoom());
		}

		private IEnumerator DoTheBoom()
		{
			transform.localScale = Vector3.one * 0.75f;

			float startTime = Time.time;
			while (Time.time - startTime < 3)
			{
				_rigidbody.velocity = transform.up * _dyingVelocity;
				transform.localRotation = Quaternion.Euler(
					transform.localEulerAngles.x + UnityEngine.Random.Range(-5, 5),
					transform.localEulerAngles.y,
					transform.localEulerAngles.z + UnityEngine.Random.Range(-5, 5)
					);
				yield return null;
			}

			startTime = Time.time;
			while (Time.time - startTime < 1)
			{
				yield return null;
			}

			// TODO: Spawn explosion
			var explosion = GameManager.Instance.ExplosionPool.GetObjectFromPool();
			explosion.transform.position = transform.position;

			ReturnSelfToObjectPool();
		}

		#region IPoolable

		private ObjectPool<DyingWasp> _objectPool;
		public void ActivatePoolable<T>(ObjectPool<T> objectPool) where T : IPoolable
		{
			_objectPool = objectPool as ObjectPool<DyingWasp>;
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
