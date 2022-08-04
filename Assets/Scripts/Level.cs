using Unity.VisualScripting;
using UnityEngine;

namespace DefaultNamespace
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private int roadLenght = 15;
        [SerializeField] private float minDamageOffset = 2;
        [SerializeField] private float maxDamageOffset = 5;

        [SerializeField] private float roadPartLenght = 5f;
        [SerializeField] private float roadPartWidth = 6f; // длинна и ширина дороги 

        [SerializeField] private GameObject roadPartPrefab;
        [SerializeField] private GameObject damagePrefab;
        [SerializeField] private GameObject finishPrefab;
        [SerializeField] private GameObject playerPrefab;

        //   private PlayerController _player;
        //  public PlayerController Player => _player;
        public PlayerController Player { get; private set; } // тоже самое что и выше

        public void Generatelevel()
        {
            Clear();
            GenerateRoad();
            GenerateDamage();
            GeneratePlayer();
        }

        private void Clear()
        {
            Player = null;
            for (int i = 0; i < transform.childCount)
        }

        private void GenerateRoad()
        {
            Vector3 roadLocalPosition = Vector3.zero;// здание обьекста в 0 позиции 
            for (int i = 0; i < roadLenght; i++)
            {
               GameObject roadPart = Instantiate(roadPartPrefab, transform);
               roadPart.transform.localPosition = roadLocalPosition;
               roadLocalPosition.z += roadPartLenght; // доб длинну 
               

            }

            GameObject finish = Instantiate(finishPrefab, transform);
            finish.transform.localPosition = roadLocalPosition;
        }

        private void GenerateDamage()
        {
            float fullLenght = roadLenght * roadPartLenght;
            float currentLenght = roadPartLenght * 2;
            float damageoffsetX = roadPartWidth / 3;
            float startPosX = roadPartWidth * 0.5f;
            
            while (currentLenght< fullLenght)
            {
                float zOffset = Random.Range(minDamageOffset, maxDamageOffset) + minDamageOffset;
                currentLenght += zOffset;
                currentLenght = Mathf.Clamp(currentLenght, 0f, fullLenght);

                int damagePosition = Random.Range(0,3);
                float damagePosX = -startPosX + damageoffsetX * damagePosition;
                GameObject damage = Instantiate(damagePrefab, transform);
                Vector3 localPosition = Vector3.zero;
                localPosition.x = damagePosX;
                localPosition.y = currentLenght;
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