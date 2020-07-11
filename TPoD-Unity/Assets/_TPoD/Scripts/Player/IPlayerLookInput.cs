using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trampoline
{
    public interface IPlayerLookInput
    {
        float GetHorizontalLook();
        float GetVerticalLook();
    }
}
