using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TPoD
{
    public class TrampolineBoosterDirection : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _arrowSprite;

        /******* Methods *******/

        public void Toggle(bool isOn) { _arrowSprite.enabled = isOn; }

        public Vector3 direction => transform.forward;
        public bool isToggledOn => _arrowSprite.enabled;

        public void SetRotation(float rotationValue)
        {
            transform.Rotate(Vector3.up, rotationValue);
        }
    }
}
