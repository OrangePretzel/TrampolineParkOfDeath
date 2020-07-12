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

            public const string CHEAT_DEAL_DAMAGE = "CheatDamage";

            public const string SHOOT = "Shoot";
            public const string REPLAY = "Replay";
            public const string QUIT = "Quit";

        }

        public class TagConstants
        {
            public const string PLAYER = "Player";
        }

        public class LayerConstants
        {
            public const string ENEMY_SHOULD_NOT_TOUCH = "EnemyShouldNotTouch";
            public const string ENEMY_COLLIDER = "EnemyCollider";
            public const string ENEMY_WEAK_SPOT = "EnemyWeakSpot";
        }
    }
}
