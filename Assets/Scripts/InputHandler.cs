using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class InputHandler : MonoBehaviour
    {
        private float _prevPosx = 0f;
        private bool _isHold;
        private float _relativeOffset=0f;

        public float HorizontalAxis => _relativeOffset;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _isHold = true;
                _prevPosx = Input.mousePosition.x;
                
            }

            if (Input.GetMouseButtonUp(0))
            {
                _isHold = false;
                _prevPosx = 0f;
                _relativeOffset = 0f;
            }

            if (_isHold)
            {
                float offset = _prevPosx - Input.mousePosition.x;
                _relativeOffset = offset / Screen.width;
                _prevPosx = Input.mousePosition.x;
            }
        }
    }
}