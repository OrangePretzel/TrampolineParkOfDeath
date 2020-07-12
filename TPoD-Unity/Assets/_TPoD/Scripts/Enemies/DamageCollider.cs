using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TPoD
{
    public class DamageCollider : MonoBehaviour
    {
        /******* Events *******/

        /******* Variables & Properties*******/
        [SerializeField] private float _damageAmount;

        /******* Monobehavior Methods *******/

        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(TrampolineConstants.TagConstants.PLAYER))
            {
                Player player = other.gameObject.GetComponent<Player>();
                player.health.DealDamage(_damageAmount);
            }
        }

        /******* Methods *******/
    }
}
