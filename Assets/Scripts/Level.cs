using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private int roadPartsCount = 15;
        [SerializeField] private float minDamageOffset = 2;
        [SerializeField] private float maxDamageOffset = 5;

        [SerializeField] private float roadPartLenght = 5f;
        [SerializeField] private float roadPartWidth = 6f;

        [SerializeField] private GameObject roadPartPrefab;
        [SerializeField] private GameObject damagePrefab;
        [SerializeField] private GameObject finishPrefab;
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private GameObject glassWallPrefab;

        // private PlayerController _player;
        //
        // public PlayerController Player => _player;
        public PlayerController Player { get; private set; }

        public void GenerateLevel()
        {
            Clear();
            GenerateRoad();
            GenerateDamage();
            GeneratePlayer();
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
            Vector3 roadLocalPosition = Vector3.zero; //стартовая позиция  (0,0,0)

            for (int i = 0; i < roadPartsCount; i++) //ген
            {
                GameObject roadPart = Instantiate(roadPartPrefab, transform); //создаём префаб
                roadPart.transform.localPosition = roadLocalPosition; //0,0,5

                roadLocalPosition.z += roadPartLenght; //0,0, 5 * 15
            }

            GameObject finish = Instantiate(finishPrefab, transform);
            finish.transform.localPosition = roadLocalPosition; //0,0,75
        }

        private void GenerateDamage()
        {
            float fullLength = roadPartsCount * roadPartLenght; //5*15 = 75
            float currentLength = roadPartLenght * 2; //5 * 2 = 10
            float damageOffsetX = roadPartWidth / 3; // 6 / 3 = 2
            float startPosX = roadPartWidth * 0.5f; // 6 * 0.5 = 3

            while (currentLength < fullLength) //10 < 75
            {
                float zOffset = Random.Range(minDamageOffset, maxDamageOffset) + minDamageOffset; // (2,5) 2.75 + 2 4.75
                currentLength += zOffset; //10 + 4.75 = 14.75 //73 + 4.75 = 77.45
                currentLength = Mathf.Clamp(currentLength, 0f, fullLength); //77.45 / 75

                List<int> tempList = new List<int>() {0, 1, 2}; //создал временный массив

                int damagePosition = Random.Range(0, 3); //0 1 2
                int tempValue = tempList[damagePosition]; // получил элемент из массива
                tempList.Remove(tempValue); // удалил полученный элемент
                float damagePosX = -startPosX + damageOffsetX * tempValue; //-3 + 2 * 0=-3 /-3 + 2 * 1=-1 /-3 + 2 * 2=-1

                GameObject damage = Instantiate(damagePrefab, transform);
                Vector3 localPosition = Vector3.zero;
                localPosition.x = damagePosX; // -3 -1 1
                localPosition.z = currentLength; // 14, 75
                damage.transform.localPosition = localPosition;

                foreach (var temp in tempList) //2 элемента остаётся
                {
                    GameObject glassWall = Instantiate(glassWallPrefab, transform);
                    localPosition = Vector3.zero;
                    damagePosX = -startPosX + damageOffsetX * temp;
                    localPosition.x = damagePosX; // -3 -1 1
                    localPosition.z = currentLength; // 14, 75
                    glassWall.transform.localPosition = localPosition;
                }
            }
        }

        private void GeneratePlayer()
        {
            GameObject player = Instantiate(playerPrefab, transform);
            player.transform.localPosition = new Vector3(0, 0, roadPartLenght * 0.5f); //5 * 0.5 = 2.5

            Player = player.GetComponent<PlayerController>();
        }
    }
}