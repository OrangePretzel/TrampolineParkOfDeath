using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TPoD
{
    public class TrampolineConstants : MonoBehaviour
    {
        public class InputConstants
        {
            public const string HORIZONTAL_MOVEMENT_AXIS = "HorizontalMovement";
            public const string VERTICAL_MOVEMENT_AXIS = "VerticalMovement";

            public const string HORIZONTAL_LOOK_AXIS = "HorizontalLook";
            public const string VERTICAL_LOOK_AXIS = "VerticalLook";
        }

        public class TagConstants
        {
            public const string PLAYER = "Player";
        }

        public class LayerConstants
        {
            public const string ENEMY_SHOULD_NOT_TOUCH = "EnemyShouldNotTouch";
        }
    }
}
