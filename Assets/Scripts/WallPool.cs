using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class WallPool : MonoBehaviour
    {
        [SerializeField] private GameObject wallPrefab;
        [SerializeField] private int wallStartCount;

        private GameObject[] _walls;

        private void Awake()
        {
            _walls = new GameObject[wallStartCount]; 
            for (int i = 0; i < wallStartCount; i++)
            {
                _walls[i] = Instantiate(wallPrefab);
                _walls[i].SetActive(false);
            }
        }

        public GameObject GetWall()
        {
            GameObject wallInstance=null;
            foreach (var wall in _walls)
            {
                if (!wall.activeInHierarchy)
                {
                    wallInstance = wall;
                    break;
                }
            }

            return wallInstance;
        }
    }
}