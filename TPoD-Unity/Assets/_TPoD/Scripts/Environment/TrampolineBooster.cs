using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TPoD
{
    public class TrampolineBooster : MonoBehaviour
    {
        /******* Events *******/

        /******* Variables & Properties*******/
        [Header("Customizable Params")]
        [SerializeField] private float _verticalBoostVelocity;
        [SerializeField] private float _horizontalBoostStartVelocity;
        [SerializeField] private float _horizontalBoostTime;

        [Header("Components")]
        [SerializeField] private TrampolineBoosterDirection _boosterDirection;

        /******* Monobehavior Methods *******/

        public void OnTriggerEnter(Collider collider)
        {
            if (collider.CompareTag(TrampolineConstants.TagConstants.PLAYER))
            {
                BoostPlayer(collider.gameObject.GetComponent<PlayerMovement>());
            }
        }

        /******* Methods *******/

        public void SetDirection(float rotationValue)
        {
            _boosterDirection.Toggle(true);
            _boosterDirection.SetRotation(rotationValue);
        }

        public void SetNoDirection()
        {
            _boosterDirection.Toggle(false);
        }

        private void BoostPlayer(PlayerMovement playerMovement)
        {
            playerMovement.AddVerticalVelocityBoost(new VerticalVelocityBoost(_verticalBoostVelocity));
            float horizontalAcceleration = -_horizontalBoostStartVelocity / _horizontalBoostTime;

            Vector3 direction = _boosterDirection.isToggledOn ? _boosterDirection.direction : playerMovement.horizontalDirection;

            playerMovement.AddHoziontalVelocityBoost(new HorizontalVelocityBoost(_horizontalBoostStartVelocity, horizontalAcceleration, direction));
        }
    }
}
