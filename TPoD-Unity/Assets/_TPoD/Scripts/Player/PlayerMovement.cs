using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TPoD
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

        private HorizontalVelocityBoost horizontalBoost = null;

        private CharacterController _characterController;
        private IPlayerMovementInput _input;

        public Vector3 horizontalDirection => new Vector3(_currentVelocityHorizontal.normalized.x, 0f, _currentVelocityHorizontal.normalized.z);

        /******* Monobehavior Methods *******/

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _input = GetComponent<IPlayerMovementInput>();
        }

        private void Update()
        {
            UpdateVelocityGravity(Time.deltaTime);
            UpdateVelocityInput(Time.deltaTime);
            UpdateVelocityBoosts(Time.deltaTime);

            UpdateMovement(Time.deltaTime);
        }

        /******* Methods *******/

        public void SetVelocity(Vector3 velocity)
        {
            _currentVelocity = velocity;
        }

        private void UpdateVelocityGravity(float deltaTime)
        {
            // 1. Gravity
            Vector3 gravityAddition = deltaTime * Physics.gravity * _gravityMultiplier;
            _currentVelocity += gravityAddition;
        }

        private void UpdateVelocityInput(float deltaTime)
        {
            // 2. Input
            Vector3 inputVector = new Vector3(_input.GetHorizontalAxis(), 0f, _input.GetVerticalAxis());
            Vector3 targetVelocityHorizontal = _strafeSpeed * ApplyTransformDirectionToInputVector(inputVector);
            _currentVelocityHorizontal = Vector3.SmoothDamp(_currentVelocityHorizontal, targetVelocityHorizontal, ref _dampingVelocity, _dampingValue);

            _currentVelocity = new Vector3(_currentVelocityHorizontal.x, _currentVelocity.y, _currentVelocityHorizontal.z);
        }

        private Vector3 ApplyTransformDirectionToInputVector(Vector3 movementInput)
        {
            return transform.TransformDirection(movementInput);
        }

        public void AddHoziontalVelocityBoost(HorizontalVelocityBoost boost)
        {
            horizontalBoost = boost;
        }

        public void AddVerticalVelocityBoost(VerticalVelocityBoost boost)
        {
            _currentVelocity = boost.velocityVector;
        }

        private void UpdateVelocityBoosts(float deltaTime)
        {
            if (horizontalBoost == null)
                return;

            bool isValid = horizontalBoost.UpdateVelocity(deltaTime);
            if (!isValid)
            {
                horizontalBoost = null;
            }
            else
            {
                _currentVelocity += horizontalBoost.currentVelocity;
            }
        }

        private void UpdateMovement(float deltaTime)
        {
            _characterController.Move(_currentVelocity * deltaTime);
        }
    }
}
