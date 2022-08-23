using System;
using System.Collections.Generic;
using DefaultNamespace;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;


public enum LevelChange
{
   None,
   LevelOne,
   LevelTwo,
   LevelThree
}
public class Level : MonoBehaviour
{
   private void Awake()
   {
      LoadLevelPrefabs(_levelPrefabs);
   }

   [SerializeField] private LevelConfig[] levelConfigs;
   [SerializeField] private LevelChange _levelChange;
   [SerializeField] private LevelPrefabs _levelPrefabs;

   private int roadLength;
   private float minDamageOffset;
   private float maxDamageOffset;
   private float roadPartLength;
   private float roadPartWidth;
   
   private GameObject roadPartPrefab;
   private GameObject damagePrefab;
   private GameObject glassPrefab;
   private GameObject finishPrefab;
   private GameObject playerPrefab;
   
   [SerializeField] private WallConfig wallConfig;
   private WallPool pool;

   public PlayerController Player { get; private set; }

   private LevelChange currentLevel;
   private LevelChange LevelChange
   
   

   {
      get => currentLevel;
      set
      {
         if(currentLevel==value)
            return;

         currentLevel = value;

         foreach (var config in levelConfigs)
         {
            if (config.ChangeLevel==currentLevel)
            {
               LoadLevelConfig(config);
            }
         }
      }
   }

   private void LoadLevelConfig(LevelConfig levelConfig)
   {
      roadLength = levelConfig.RoadLength;
      minDamageOffset = levelConfig.MinDamageOffset;
      maxDamageOffset = levelConfig.MaxDamageOffset;
      roadPartLength = levelConfig.RoadPartLength;
      roadPartWidth = levelConfig.RoadPartWidth;
   }

   private void LoadLevelPrefabs(LevelPrefabs levelPrefabs)
   {
      roadPartPrefab = levelPrefabs.RoadPartPrefab;
      damagePrefab = levelPrefabs.DamagePrefab;
      finishPrefab = levelPrefabs.FinishPrefab;
      playerPrefab = levelPrefabs.PlayerPrefab;
      glassPrefab = levelPrefabs.GlassPrefab;

   }
   
   public void GenerateLevel()
   {
      LevelChange = _levelChange;
      Clear();
      GenerateRoad();
      GenerateDamage();
      GeneratePlayer();
   }

   public void RestartLevel()
   {
      ReturnGlass();
      DestroyPlayer();
      GeneratePlayer();
   }
   
      public void ReturnGlass()
      {
         int count = transform.childCount;
         for (int i = 0; i < count; i++)
         {
            if (transform.GetChild(i).GetChild(0).GetComponent<GlassWall>())
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

         //GameObject damage = pool.GetWall();
         GameObject damage = Instantiate(damagePrefab, transform);
         Vector3 localPosition = Vector3.zero;
         localPosition.x = damagePosX; 
         localPosition.z = currentLength; 
         damage.transform.localPosition = localPosition;
         //damage.SetActive(true);

         damage.GetComponentInChildren<MeshRenderer>().material.color=wallConfig.GetRandomColor();

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
