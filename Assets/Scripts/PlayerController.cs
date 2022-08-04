using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerController : MonoBehaviour
    {
        private const string RUN = "Run";
        private const string FAIL = "Fail";
        private const string DANCE = "Dance ";
        private static readonly int Run = Animator
        private static readonly int
        private static readonly int
        [SerializeField] private float roadWidth = 3;
        [SerializeField] private float forwardSpeed = 5f;
        [SerializeField] private InputHandler inputHandler;
        [SerializeField] private float turnRotationAngle = 20;
        [SerializeField] private float  lerpSpeed = 5 ;
        [SerializeField] private Transform view;
        
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
            float offset = inputHandler.HorizontalAxis * roadWidth;
            Vector3 position = transform.localPosition;
            position.x += offset;
            float roadDelta = roadWidth * 0.5f;
            position.x = Mathf.Clamp(position.x, -roadDelta, roadDelta);

            Vector3 rotation = view.localRotation.eulerAngles;
            rotation.y = Mathf.LerpAngle(rotation.y, offset != 0 ? Mathf.Sign(offset) * turnRotationAngle: 0,)
                lerpSpeed * Time.deltaTime;
            view.localRotation = Quaternion.Euler(rotation);
            
            transform.localPosition = position;
            //fps == 60 dt = 0.01666 // 0.01666 * 60 == 1
            //fps == 120 dt == 0.008333; // 0.008333 * 120 == 1
            
            //speed == 5 
            //5 * dt == 
            transform.Translate(transform.forward * forwardSpeed * Time.deltaTime);
        }
    }
}