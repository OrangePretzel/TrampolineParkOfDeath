using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TPoD
{
    public class VerticalVelocityBoost
    {
        public Vector3 velocityVector { get; private set; }

        public VerticalVelocityBoost(float velocity)
        {
            velocityVector = new Vector3(0f, velocity, 0f);
        }
    }
}
