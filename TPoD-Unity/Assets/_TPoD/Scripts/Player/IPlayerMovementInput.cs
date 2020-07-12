using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TPoD
{
    public interface IPlayerMovementInput
    {
        float GetHorizontalAxis();
        float GetVerticalAxis();
    }
}
