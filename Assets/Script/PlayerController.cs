using System.Collections;
using UnityEngine;

namespace DefaultNameSpace
{
    public class PlayerController: MonoBehaviour
    {
        [SerializeField] private float forwardSpeed = 5f;
        [SerializeField] private InputHandler inputHandler;
        [SerializeField] private float roadWidth = 3;
        private bool _isActiv;

        private void Start()
        {
            _isActiv = true;
        }

        private void Update()
        {
            if (!_isActiv)
                return;

            Move();

        }
        private void Move()
        {
            float offset = inputHandler.HorizontalAxis;
            Vector3 position = transform.localPosition;
            position.x += offset;
            float roadDelta = roadWidth * 0.5f;
            position.x = Mathf.Clamp(value: position.x, min:-roadDelta, max:roadDelta);
            transform.localPosition = position;

            transform.Translate(translation:transform.forward * forwardSpeed * Time.deltaTime);

        }
    }
    

    
}