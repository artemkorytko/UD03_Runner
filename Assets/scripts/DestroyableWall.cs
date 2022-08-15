using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableWall : MonoBehaviour
{
    [SerializeField] private GameObject effectPrefab;
    [SerializeField] private GameObject cubes;

    private void Start()
    {
        effectPrefab.SetActive(false);
        cubes.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            gameObject.SetActive(false);
            effectPrefab.SetActive(true);
            cubes.SetActive(true);
        }
    }
}
