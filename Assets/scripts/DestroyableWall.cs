using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableWall : MonoBehaviour
{
    [SerializeField] private GameObject effectPrefab;
    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Instantiate(effectPrefab,transform);
            Destroy(gameObject);
        }
    }
}
