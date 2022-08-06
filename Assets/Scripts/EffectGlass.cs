using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectGlass : MonoBehaviour
{
   [SerializeField] private GameObject GlassEffectPrefab;

   private void OnTriggerEnter(Collider other)
   {
      other.CompareTag("Player");
      Instantiate(GlassEffectPrefab, transform.position, Quaternion.identity);
   }
}
