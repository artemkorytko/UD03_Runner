using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubesDestroer : MonoBehaviour
{
    [SerializeField] private float timerForDestroy = 2f;
    private void Start()
    {
        StartCoroutine(DestroyThisWall(timerForDestroy));
    }

    IEnumerator DestroyThisWall(float timer)
    {
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
    }
    
}
