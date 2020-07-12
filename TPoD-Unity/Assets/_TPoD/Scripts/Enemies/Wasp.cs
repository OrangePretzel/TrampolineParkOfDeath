using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Metamesa.MMUnity.ObjectPooling;

namespace TPoD
{
    public class Wasp : MonoBehaviour, IPoolable
    {
        /******* Events *******/

        /******* Variables & Properties*******/
        [SerializeField] public WaspEnemyAnimator animator;
        [SerializeField] public WaspAI ai;
        [SerializeField] public Health health;

		/******* Monobehavior Methods *******/

		/******* Methods *******/
		#region IPoolable

		public void ActivatePoolable<T>(ObjectPool<T> objectPool) where T : IPoolable
		{
			this.gameObject.SetActive(true);
			health.ResetHealth();
			ai.StartAI();
		}

		public void DeactivatePoolable()
		{
			ai.StopAI();
			this.gameObject.SetActive(false);
		}

		#endregion
	}
}
