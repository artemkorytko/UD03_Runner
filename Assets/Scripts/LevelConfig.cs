using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "Configs/LevelConfig", order = 0)]
    public class LevelConfig : ScriptableObject
    {
        [SerializeField] private int roadLength = 15;
        [SerializeField] private float minDamageOffset = 2;
        [SerializeField] private float maxDamageOffset = 5;
        [SerializeField] private float roadPartLength = 5f;
        [SerializeField] private float roadPartWidth = 6f;
        [SerializeField] private LevelChange changeLevel;
        
        public int RoadLength => this.roadLength;
        public float MinDamageOffset => this.minDamageOffset;
        public float MaxDamageOffset => this.maxDamageOffset;
        public float RoadPartLength => this.roadPartLength;
        public float RoadPartWidth => this.roadPartWidth;
        public LevelChange ChangeLevel => this.changeLevel;

    }
}