using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trampoline
{
    public class HorizontalVelocityBoost
    {
        private float _startingSpeed;
        // Acceleration of the horizontal boost. Usually set this to a negative value in order for the velocity to decrease over time."
        private float _acceleration;

        private Vector3 _direction;

        private float _currentSpeed;
        public Vector3 currentVelocity => _currentSpeed * _direction;

        public HorizontalVelocityBoost(float startingSpeed, float acceleration, Vector3 direction)
        {
            _startingSpeed = startingSpeed;
            _acceleration = acceleration;
            _currentSpeed = _startingSpeed;
            _direction = direction.normalized;
        }

        /// <summary>
        /// Updates the velocity by the delta time and then returns if the velocity is valid anymore (whether there is any boost being applied anymore)
        /// </summary>
        /// <param name="deltaTime"></param>
        /// <returns></returns>
        public bool UpdateVelocity(float deltaTime)
        {
            _currentSpeed += _acceleration * deltaTime;
            return _currentSpeed > 0f;
        }
    }
}
