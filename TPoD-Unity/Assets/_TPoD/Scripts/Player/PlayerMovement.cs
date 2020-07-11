using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trampoline
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(IPlayerMovementInput))]
    public class PlayerMovement : MonoBehaviour
    {
        /******* Events *******/

        /******* Variables & Properties*******/

        [Header("Customizable Params")]
        [SerializeField] private float _strafeAcceleration;
        [SerializeField] private float _maxStrafeVelocity;
        [SerializeField] private float _gravityMultiplier = 1f;

        private Vector3 _currentVelocity;

        private CharacterController _characterController;
        private IPlayerMovementInput _input;

        /******* Monobehavior Methods *******/

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _input = GetComponent<IPlayerMovementInput>();
        }

        private void Update()
        {
            UpdateVelocity(Time.deltaTime);
            UpdateMovement(Time.deltaTime);
        }

        /******* Methods *******/

        public void SetVelocity(Vector3 velocity)
        {
            _currentVelocity = velocity;
        }

        private void UpdateVelocity(float deltaTime)
        {
            // 1. Gravity
            Vector3 gravityDiff = deltaTime * Physics.gravity * _gravityMultiplier;

            // 2. Input
            Vector3 inputVector = new Vector3(_input.GetHorizontalAxis(), 0f, _input.GetVerticalAxis());
            Vector3 inputDiff = deltaTime * _strafeAcceleration * ApplyTransformDirectionToInputVector(inputVector);

            float xResult = Mathf.Clamp(_currentVelocity.x + inputDiff.x, -_maxStrafeVelocity, _maxStrafeVelocity);
            float zResult = Mathf.Clamp(_currentVelocity.z + inputDiff.z, -_maxStrafeVelocity, _maxStrafeVelocity);
            float yResult = _currentVelocity.y + gravityDiff.y;

            _currentVelocity = new Vector3(xResult, yResult, zResult);
        }

        private Vector3 ApplyTransformDirectionToInputVector(Vector3 movementInput)
        {
            return transform.InverseTransformDirection(movementInput);
        }

        private void UpdateMovement(float deltaTime)
        {
            _characterController.Move(_currentVelocity * deltaTime);
        }
    }
}
