using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class EffectWall : MonoBehaviour
{
   [SerializeField] private GameObject wallEffectPrefab;

   private void OnCollisionEnter(Collision collision)
   {
      if (collision.gameObject.GetComponent<PlayerController>())
      {
         CrashWall();
      }
   }

   public void CrashWall()
   {
      Instantiate(wallEffectPrefab, transform.position, Quaternion.identity);
   }
}
