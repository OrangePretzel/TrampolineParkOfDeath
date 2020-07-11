﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trampoline
{
    public interface IPlayerMovementInput
    {
        float GetHorizontalAxis();
        float GetVerticalAxis();
    }
}
