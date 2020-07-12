using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Metamesa.MMUnity.ObjectPooling;

namespace TPoD
{
    [RequireComponent(typeof(Collider))]
    public class WaspProjectile : MonoBehaviour, IPoolable
    {
        /******* Events *******/

        /******* Variables & Properties*******/
        [SerializeField] private float _speed;
        [SerializeField] private float _damage;
        [SerializeField] private float _maxBulletTime;

        private Vector3 velocity;

        private Coroutine shootRoutine;

        /******* Monobehavior Methods *******/

        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(TrampolineConstants.TagConstants.PLAYER))
            {
                Player player = other.gameObject.GetComponent<Player>();
                player.health.DealDamage(_damage);

                StopCoroutine(shootRoutine);
                // At the end put back in pool
                _pool.ReturnObjectToPool(this);
            }
        }

        /******* Methods *******/

        public void Shoot(Vector3 direction)
        {
            velocity = direction.normalized * _speed;
            shootRoutine = StartCoroutine(ShootCoroutine());
        }

        private IEnumerator ShootCoroutine()
        {
            float time = 0f;
            while (time < _maxBulletTime)
            {
                time += Time.deltaTime;
                transform.Translate(velocity * Time.deltaTime);
                yield return null;
            }
            // At the end put back in pool
            _pool.ReturnObjectToPool(this);
        }

        #region IPoolable

        private ObjectPool<WaspProjectile> _pool;

        public void ActivatePoolable<T>(ObjectPool<T> objectPool) where T : IPoolable
        {
            gameObject.SetActive(true);
            _pool = objectPool as ObjectPool<WaspProjectile>;
        }

        public void DeactivatePoolable()
        {
            gameObject.SetActive(false);
            velocity = Vector3.zero;
        }

#endregion
    }
}
