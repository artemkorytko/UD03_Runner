using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "Configs/LevelConfig", order = 0)]
    public class LevelConfig : ScriptableObject
    {
        [SerializeField] private LevelDifficulty difficulty;
        
        [SerializeField] private int roadLenght = 15;
        [SerializeField] private float minDamageOffset = 2;
        [SerializeField] private float maxDamageOffset = 5;
        [SerializeField] private float roadPartLenght = 5f;
        [SerializeField] private float roadPartWidth = 6f;

        public LevelDifficulty Difficulty => difficulty;
        public int RoadLenght => roadLenght;
        public float MinDamageOffset => minDamageOffset;
        public float MaxDamageOffset => maxDamageOffset;
        public float RoadPartLenght => roadPartLenght;
        public float RoadPartWidth => roadPartWidth;
    }
}