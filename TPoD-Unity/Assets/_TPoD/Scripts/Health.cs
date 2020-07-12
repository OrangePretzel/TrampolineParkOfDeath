using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Trampoline
{
    public class Health : MonoBehaviour
    {
        /******* Events *******/

        public delegate void OnDamageDone(float damageValue, float newHealth);
        public event OnDamageDone onDamageDone;

        public delegate void OnDeath(GameObject gameObject);
        public event OnDeath onDeath;

        /******* Variables & Properties*******/
        [SerializeField] private float _startingHealth;

        public float health { get; private set; }

        /******* Monobehavior Methods *******/

        public void Awake()
        {
            health = _startingHealth;
        }

        /******* Methods *******/

        public void DoDamage(float damage)
        {
            health -= damage;
            onDamageDone?.Invoke(damage, health);
            if (health <= 0f)
            {
                onDeath?.Invoke(gameObject);
            }
        }
    }
}
