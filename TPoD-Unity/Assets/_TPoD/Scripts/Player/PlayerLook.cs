using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TPoD
{
    [RequireComponent(typeof(IPlayerLookInput))]
    public class PlayerLook : MonoBehaviour
    {
        /******* Events *******/

        /******* Variables & Properties*******/
        [SerializeField] private float _lookSensitivity;
        [SerializeField] private float _dampValue;

        [SerializeField] private GameObject _verticalRotationContainer;

        private IPlayerLookInput _input;

        private float _targetVerticalRotation;
        private float _actualVerticalRotation;
        private float _verticalRotationVelocity;

        private float _targetHorizontalRotation;
        private float _actualHorizontalRotation;
        private float _horizontalRotationVelocity;

        /******* Monobehavior Methods *******/

        public void Awake()
        {
            _input = GetComponent<IPlayerLookInput>();

            Cursor.lockState = CursorLockMode.Locked;
        }

        public void Update()
        {
            UpdateRotation(Time.deltaTime);
        }

        /******* Methods *******/

        private void UpdateRotation(float deltaTime)
        {
            _targetVerticalRotation = Mathf.Clamp(_targetVerticalRotation + (_input.GetVerticalLook() * _lookSensitivity), -90f, 90f);
            _actualVerticalRotation = Mathf.SmoothDamp(_actualVerticalRotation, _targetVerticalRotation, ref _verticalRotationVelocity, _dampValue);

            _targetHorizontalRotation += _input.GetHorizontalLook() * _lookSensitivity;
            _actualHorizontalRotation = Mathf.SmoothDamp(_actualHorizontalRotation, _targetHorizontalRotation, ref _horizontalRotationVelocity, _dampValue);

            _verticalRotationContainer.transform.localRotation = Quaternion.Euler(-_actualVerticalRotation, 0, 0);
            transform.rotation = Quaternion.Euler(0, _actualHorizontalRotation, 0);
        }
    }
}
