using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectWall : MonoBehaviour
{
   [SerializeField] private GameObject WallEffectPrefab;

   private void OnCollisionEnter(Collision collision)
   {
      collision.gameObject.CompareTag("Player");
      Instantiate(WallEffectPrefab, transform.position, Quaternion.identity);
   }
}
