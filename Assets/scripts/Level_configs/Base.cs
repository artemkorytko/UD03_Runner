using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Level_configs
{
    public abstract class Base : ScriptableObject
    {
        [SerializeField] private Difficults difficults;
        
        [SerializeField] private int roadLenght = 15;
        [SerializeField] private float minDamageOffset = 2f;
        [SerializeField] private float maxDamageOffset = 5f;
    
        [SerializeField] private float roadPartLenght = 5f;
        [SerializeField] private float roadPartWidth = 6f;

        [SerializeField] private int countOfWallsInLine = 3;

        [SerializeField] private GameObject roadPartPrefab;
        [SerializeField] private GameObject finishPrefab;
    
        public Difficults Difficult => difficults;
        
        //public int RoadLenght => roadLenght;

        public int RoadLenght => roadLenght;
        public float MinDamageOffset => minDamageOffset;
        public float MaxDamageOffset => maxDamageOffset;
        
        public float RoadPartLenght => roadPartLenght;
        public float RoadPartWidth => roadPartWidth;
        
        public int CountOfWallsInLine => countOfWallsInLine;
        
        public GameObject RoadPartPrefab => roadPartPrefab;
        public GameObject FinishPrefab => finishPrefab;
        
    }
}
