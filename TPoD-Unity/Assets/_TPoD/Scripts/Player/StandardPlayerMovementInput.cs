﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TPoD
{
    public class StandardPlayerMovementInput : MonoBehaviour, IPlayerMovementInput
    {
        /******* Methods *******/
        public float GetHorizontalAxis()
        {
            return Input.GetAxis(TrampolineConstants.InputConstants.HORIZONTAL_MOVEMENT_AXIS);
        }

        public float GetVerticalAxis()
        {
            return Input.GetAxis(TrampolineConstants.InputConstants.VERTICAL_MOVEMENT_AXIS);
        }
    }
}
