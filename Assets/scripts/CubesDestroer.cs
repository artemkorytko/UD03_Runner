using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubesDestroer : MonoBehaviour
{
    private float _timer;
    private void Start()
    {
        _timer = 2f;
    }

    private void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer<0)
        {
            Destroy(gameObject);
        }
    }
}
