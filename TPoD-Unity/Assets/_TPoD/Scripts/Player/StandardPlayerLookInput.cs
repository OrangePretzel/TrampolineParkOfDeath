using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trampoline
{
    public class StandardPlayerLookInput : MonoBehaviour, IPlayerLookInput
    {
        public float GetHorizontalLook()
        {
            return Input.GetAxis(TrampolineConstants.InputConstants.HORIZONTAL_LOOK_AXIS);
        }

        public float GetVerticalLook()
        {
            return Input.GetAxis(TrampolineConstants.InputConstants.VERTICAL_LOOK_AXIS);
        }
    }
}
