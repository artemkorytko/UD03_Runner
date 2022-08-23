using Unity.VisualScripting;
using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "LevelPrefabs", menuName = "Configs/LevelPrefabs", order = 0)]
    public class LevelPrefabs : ScriptableObject
    {
        [SerializeField] private GameObject roadPartPrefab;
        [SerializeField] private GameObject damagePrefab;
        [SerializeField] private GameObject glassPrefab;
        [SerializeField] private GameObject finishPrefab;
        [SerializeField] private GameObject playerPrefab;

        public GameObject RoadPartPrefab => this.roadPartPrefab;
        public GameObject DamagePrefab => this.damagePrefab;
        public GameObject GlassPrefab => this.glassPrefab;
        public GameObject FinishPrefab => this.finishPrefab;
        public GameObject PlayerPrefab => this.playerPrefab;

        
    }
}