using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "WallConfig", menuName = "Configs/WallConfig")]
    public class WallConfig : ScriptableObject
    {
        [SerializeField] private Color[] colors;

        public Color GetRandomColor()
        {
            return colors[Random.Range(0, colors.Length)];
        }
    }
}