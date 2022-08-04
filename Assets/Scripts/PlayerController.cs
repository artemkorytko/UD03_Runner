using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float roadWidth = 3;
        [SerializeField] private float forwardSpeed = 5f;
        [SerializeField] private InputHandler inputHandler;

        private bool _isActive;

        private void Start()
        {
            _isActive = true;//for debug
        }

        private void Update()
        {
            if (!_isActive)
                return;

            Move();
        }

        private void Move()
        {
            //horizontal move
            float offset = inputHandler.HorizontalAxis;
            Vector3 position = transform.localPosition;
            position.x += offset;
            float roadDelta = roadWidth * 0.5f;
            position.x = Mathf.Clamp(position.x, -roadDelta, roadDelta);
            transform.localPosition = position;
            //fps == 60 dt = 0.01666 // 0.01666 * 60 == 1
            //fps == 120 dt == 0.008333; // 0.008333 * 120 == 1
            
            //speed == 5 
            //5 * dt == 
            transform.Translate(transform.forward * forwardSpeed * Time.deltaTime);
        }
    }
}