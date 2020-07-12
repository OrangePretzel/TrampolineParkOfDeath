using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TPoD
{
    public class Player : MonoBehaviour
    {
        /******* Events *******/

        /******* Variables & Properties*******/
        public PlayerMovement movement;
        public PlayerLook look;
        public Health health;

		public void AddGunKnockback()
		{
            // TODO: Add gun knockback
            //transform.forward * -30
		}
	}
}
