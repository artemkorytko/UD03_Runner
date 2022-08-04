using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class InputHandler : MonoBehaviour
    {
        private float _prevPosX = 0f;
        private bool _isHold;
        private float _relativeOffset = 0f;

        public float HorizontalAxis => -_relativeOffset; 

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _isHold = true;
                _prevPosX = Input.mousePosition.x;
            }

            if (Input.GetMouseButtonUp(0))
            {
                _isHold = false;
                _prevPosX = 0;
            }

            if (_isHold)
            {
                float offset = _prevPosX - Input.mousePosition.x;
                _relativeOffset = offset / Screen.width;
                _prevPosX = Input.mousePosition.x;
            }
        }
    }
}