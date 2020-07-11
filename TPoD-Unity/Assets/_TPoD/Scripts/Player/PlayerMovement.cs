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
        [SerializeField] private float _strafeSpeed;
        [SerializeField] private float _gravityMultiplier = 1f;
        [SerializeField] private float _dampingValue;

        private Vector3 _currentVelocity;
        private Vector3 _currentVelocityHorizontal;
        private Vector3 _dampingVelocity;

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
            Vector3 gravityAddition = deltaTime * Physics.gravity * _gravityMultiplier;
            float yResult = _currentVelocity.y + gravityAddition.y;

            // 2. Input
            Vector3 inputVector = new Vector3(_input.GetHorizontalAxis(), 0f, _input.GetVerticalAxis());
            Vector3 targetVelocityHorizontal = _strafeSpeed * ApplyTransformDirectionToInputVector(inputVector);
            _currentVelocityHorizontal = Vector3.SmoothDamp(_currentVelocityHorizontal, targetVelocityHorizontal, ref _dampingVelocity, _dampingValue);
            Debug.Log(targetVelocityHorizontal + "    " + _currentVelocityHorizontal);

            _currentVelocity = new Vector3(_currentVelocityHorizontal.x, yResult, _currentVelocityHorizontal.z);
        }

        private Vector3 ApplyTransformDirectionToInputVector(Vector3 movementInput)
        {
            return transform.TransformDirection(movementInput);
        }

        private void UpdateMovement(float deltaTime)
        {
            _characterController.Move(_currentVelocity * deltaTime);
        }
    }
}
