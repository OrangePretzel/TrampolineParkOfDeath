using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trampoline
{
    public class VerticalVelocityBoost : MonoBehaviour
    {
        public Vector3 velocityVector { get; private set; }

        public VerticalVelocityBoost(float velocity)
        {
            velocityVector = new Vector3(0f, velocity, 0f);
        }
    }
}
