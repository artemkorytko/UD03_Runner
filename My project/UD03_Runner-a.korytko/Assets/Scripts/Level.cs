using UnityEngine;

namespace DefaultNamespace
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private int roadLenght = 15;
        [SerializeField] private float minDamageOffset = 2;
        [SerializeField] private float maxDamageOffset = 5;

        [SerializeField] private float roadPartLenght = 5f;
        [SerializeField] private float roadPartWidth = 6f;

        [SerializeField] private GameObject roadPartPrefab;
        [SerializeField] private GameObject damagePrefab;
        [SerializeField] private GameObject finishPrefab;
        [SerializeField] private GameObject playerPrefab;

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
            Vector3 roadLocalPosition = Vector3.zero;//стартовая позиция

            for (int i = 0; i < roadLenght; i++) //ген
            {
                GameObject roadPart = Instantiate(roadPartPrefab, transform);//создаём префаб
                roadPart.transform.localPosition = roadLocalPosition;

                roadLocalPosition.z += roadPartLenght;
            }

            GameObject finish = Instantiate(finishPrefab, transform);
            finish.transform.localPosition = roadLocalPosition;
        }

        private void GenerateDamage()
        {
            float fullLength = roadLenght * roadPartLenght;
            float currentLength = roadPartLenght * 2;
            float damageOffsetX = roadPartWidth / 3;
            float startPosX = roadPartWidth * 0.5f;

            while (currentLength < fullLength)
            {
                float zOffset = Random.Range(minDamageOffset, maxDamageOffset) + minDamageOffset;
                currentLength += zOffset;
                currentLength = Mathf.Clamp(currentLength, 0f, fullLength);

                int damagePosition = Random.Range(0, 3);
                float damagePosX = -startPosX + damageOffsetX * damagePosition;

                GameObject damage = Instantiate(damagePrefab, transform);
                Vector3 localPosition = Vector3.zero;
                localPosition.x = damagePosX;
                localPosition.z = currentLength;
                damage.transform.localPosition = localPosition;
            }
        }

        private void GeneratePlayer()
        {
            GameObject player = Instantiate(playerPrefab, transform);
            player.transform.localPosition = new Vector3(0, 0, roadPartLenght * 0.5f);

            Player = player.GetComponent<PlayerController>();
        }
    }
}