using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float forwardSpeed = 5f;
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private float roadWidth = 3f;

    private bool _isActive;

    private void Start()
    {
        _isActive = true; //debug
    }

    private void Update()
    {
        if (!_isActive)
            return;

        Move();
    }

    private void Move()
    {
        float offset = inputHandler.HorizontalAxis;
        Vector3 position = transform.localPosition;
        position.x += offset;
        float roadDelta = roadWidth / 2;
        position.x = Mathf.Clamp(position.x, -roadDelta, roadDelta);
        transform.localPosition = position;
        
        transform.Translate(transform.forward * (forwardSpeed * Time.deltaTime));
    }
}
