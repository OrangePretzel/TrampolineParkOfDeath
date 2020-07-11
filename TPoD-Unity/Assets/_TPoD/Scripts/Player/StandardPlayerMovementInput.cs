using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trampoline
{
    public class StandardPlayerMovementInput : MonoBehaviour, IPlayerMovementInput
    {
        /******* Methods *******/
        public float GetHorizontalAxis()
        {
            return Input.GetAxis(TrampolineConstants.InputConstants.HORIZONTAL_AXIS);
        }

        public float GetVerticalAxis()
        {
            return Input.GetAxis(TrampolineConstants.InputConstants.VERTICAL_AXIS);
        }
    }
}
