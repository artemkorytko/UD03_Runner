using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float RoadWidth = 3;
    [SerializeField] private float ForwardSpeed = 5f;
    [SerializeField] private InputHandler InputHandler;

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
        float offset = InputHandler.HorizontalAxis;
        Vector3 position = transform.localPosition;
        position.x += offset;
        float roadDelta = RoadWidth * 0.5f;
        position.x = Mathf.Clamp(position.x, -roadDelta, roadDelta);
        transform.localPosition = position;
        transform.Translate(transform.forward * (ForwardSpeed * Time.deltaTime));
    }
}

