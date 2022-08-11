using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using Random = UnityEngine.Random;

public class Level : MonoBehaviour
{
   [SerializeField] private int roadLength = 15;
   [SerializeField] private float minDamageOffset = 2;
   [SerializeField] private float maxDamageOffset = 5;
   
   [SerializeField] private float roadPartLength = 5f;
   [SerializeField] private float roadPartWidth = 6f;

   [SerializeField] private GameObject roadPartPrefab;
   [SerializeField] private GameObject damagePrefab;
   [SerializeField] private GameObject glassPrefab;
   [SerializeField] private GameObject finishPrefab;
   [SerializeField] private GameObject playerPrefab;
   
   public PlayerController Player { get; private set; }

   public void GenerateLevel()
   {
      Clear();
      GenerateRoad();
      GenerateDamage();
      GeneratePlayer();
   }

   public void RestartLevel()
   {
      DestroyPlayer();
      GeneratePlayer();
   }


   public void ReturnGlass()
   {
      int count = transform.childCount;
      for (int i = 0; i < count; i++)
      {
         if (transform.GetChild(i).gameObject.CompareTag("Glass"))
         {
            GameObject child = transform.GetChild(i).gameObject;
            Vector3 position = child.transform.localPosition;
            Destroy(child);
            GameObject newGlass = Instantiate(glassPrefab, transform);
            newGlass.transform.localPosition = position;
         }
      }

   }

   private void DestroyPlayer()
   {
      Destroy(Player.gameObject);
   }

   private void Clear()
   {
      Player = null;
      for (int i = 0; i < transform.childCount; i++)
      {
         Destroy(transform.GetChild(i).gameObject);
      }
   }

   private void GenerateRoad()
   {
      Vector3 roadLocalPosition = Vector3.zero;

      for (int i = 0; i < roadLength; i++)
      {
         GameObject roadPart=Instantiate(roadPartPrefab, transform);
         roadPart.transform.localPosition = roadLocalPosition;
         roadLocalPosition.z += roadPartLength;
      }

      GameObject finish = Instantiate(finishPrefab, transform);
      finish.transform.localPosition = roadLocalPosition;
   }
   private void GenerateDamage()
   {
      float fullLength = roadLength * roadPartLength;
      float currentLength = roadPartLength * 2;
      float damageOffsetX = roadPartWidth / 3;
      float startPosX = roadPartWidth * 0.5f; 

      while (currentLength < fullLength) 
      {
         float zOffset = Random.Range(minDamageOffset, maxDamageOffset) + minDamageOffset; 
         currentLength += zOffset;
         currentLength = Mathf.Clamp(currentLength, 0f, fullLength);

         List<int> tempList = new List<int>() {0, 1, 2}; 

         int damagePosition = Random.Range(0, 3); 
         int tempValue = tempList[damagePosition]; 
         tempList.Remove(tempValue);
         float damagePosX = -startPosX + damageOffsetX * tempValue;

         GameObject damage = Instantiate(damagePrefab, transform);
         Vector3 localPosition = Vector3.zero;
         localPosition.x = damagePosX; // -3 -1 1
         localPosition.z = currentLength; // 14, 75
         damage.transform.localPosition = localPosition;

         foreach (var temp in tempList)
         {
            GameObject glassWall = Instantiate(glassPrefab, transform);
            localPosition = Vector3.zero;
            damagePosX = -startPosX + damageOffsetX * temp;
            localPosition.x = damagePosX;
            localPosition.z = currentLength;
            glassWall.transform.localPosition = localPosition;
         }
      }
   }

      private void GeneratePlayer()
   {
      GameObject player = Instantiate(playerPrefab, transform);
      player.transform.localPosition = new Vector3(0, 0, roadPartLength * 0.5f);
      Player = player.GetComponent<PlayerController>();
   }
}
