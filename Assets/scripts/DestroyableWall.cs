using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableWall : MonoBehaviour
{
    [SerializeField] private GameObject effectPrefab;
    [SerializeField] private GameObject cubes;

    private bool _isActive = false;
    private float _timer = 2f;
    private void Update()
    {
        if (_isActive)
        {
            _timer -= Time.deltaTime;
            if (_timer<0f)
            {
                cubes.SetActive(false);
                effectPrefab.SetActive(false);
                _isActive = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            effectPrefab.SetActive(true);
            cubes.SetActive(true);
            _isActive = true;
        }
    }
}
