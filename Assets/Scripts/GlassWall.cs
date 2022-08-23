using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class GlassWall : MonoBehaviour
{
   [SerializeField] private GameObject glassWall;
   [SerializeField] private GameObject fx;

   private void OnTriggerEnter(Collider other)
   {
      if (other.gameObject.GetComponent<PlayerController>())
      {
         CrashGlassWall();
      }
   }

   public void CrashGlassWall()
   {
      glassWall.gameObject.SetActive(false);
      fx.gameObject.SetActive(true);
   }

   public void NotCrashGlassWall()
   {
      glassWall.gameObject.SetActive(true);
      fx.gameObject.SetActive(false);
   }
}
