using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trampoline
{
    public class TrampolineBooster : MonoBehaviour
    {
        /******* Events *******/

        /******* Variables & Properties*******/
        [SerializeField] private float _verticalBoostVelocity;
        [SerializeField] private float _horizontalBoostStartVelocity;
        [SerializeField] private float _horizontalBoostTime;

        /******* Monobehavior Methods *******/

        public void OnTriggerEnter(Collider collider)
        {
            if (collider.CompareTag(TrampolineConstants.TagConstants.PLAYER))
            {
                BoostPlayer(collider.gameObject.GetComponent<PlayerMovement>());
            }
        }

        /******* Methods *******/

        private void BoostPlayer(PlayerMovement playerMovement)
        {
            playerMovement.AddVerticalVelocityBoost(new VerticalVelocityBoost(_verticalBoostVelocity));
            float horizontalAcceleration = -_horizontalBoostStartVelocity / _horizontalBoostTime;
            playerMovement.AddHoziontalVelocityBoost(new HorizontalVelocityBoost(_horizontalBoostStartVelocity, horizontalAcceleration, playerMovement.horizontalDirection));
        }
    }
}
