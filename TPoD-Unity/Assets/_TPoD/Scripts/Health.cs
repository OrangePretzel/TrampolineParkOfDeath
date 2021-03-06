﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace TPoD
{
    public class Health : MonoBehaviour
    {
        /******* Events *******/

        public delegate void OnDamageTaken(float damageValue, float newHealth);
        public event OnDamageTaken onDamageTaken;

        public delegate void OnDeath(GameObject gameObject);
        public event OnDeath onDeath;

        /******* Variables & Properties*******/
        [SerializeField] private float _startingHealth;

        public float health { get; private set; }

        private bool _isInvincible = false;

        /******* Monobehavior Methods *******/

        public void Awake()
        {
            health = _startingHealth;
        }

        /******* Methods *******/

        public void ToggleInvincibility(bool isInvincible, float length = -1)
        {
            _isInvincible = isInvincible;
            if (length != -1)
            {
                StartCoroutine(WaitThenSetInvincibility(length, !isInvincible));
            }
        }

        private IEnumerator WaitThenSetInvincibility(float time, bool toggleValue)
        {
            yield return new WaitForSeconds(time);
            ToggleInvincibility(toggleValue);
        }

        public void ResetHealth()
        {
            health = _startingHealth;
        }

        public void DealDamage(float damage)
        {
            if (_isInvincible)
                return;

            health -= damage;
            onDamageTaken?.Invoke(damage, health);
            if (health <= 0f)
            {
                onDeath?.Invoke(gameObject);
            }
        }
    }
}
