using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "LevelPrefabs", menuName = "Configs/LevelPrefabs", order = 0)]
    public class LevelPrefabs : ScriptableObject
    {
        [SerializeField] private GameObject roadPart;
        [SerializeField] private GameObject damageWall;
        [SerializeField] private GameObject noDamageWall;
        [SerializeField] private GameObject finish;
        [SerializeField] private GameObject player;

        public GameObject RoadPart => roadPart;
        public GameObject DamageWall => damageWall;
        public GameObject NoDamageWall => noDamageWall;
        public GameObject Finish => finish;
        public GameObject Player => player;
    }
}