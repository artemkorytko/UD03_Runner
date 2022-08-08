using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private int roadLenght = 15;
    [SerializeField] private float minDamageOffset = 2;
    [SerializeField] private float maxDamageOffset = 5;

    [SerializeField] private float roadPartLenght = 5f;
    [SerializeField] private float roadPartWidth = 6f;

    [SerializeField] private GameObject roadPartPrefab;
    [SerializeField] private GameObject damagePrefab;
    [SerializeField] private GameObject noDamagePrefab;
    [SerializeField] private GameObject finishPrefab;
    [SerializeField] private GameObject playerPrefab;

    public PlayerController Player { get; private set; }

    private GameObject _currentRoad;
    private GameObject _currentDamage;

    public void GenerateLevel()
    {
        Clear();
        GenerateRoad();
        GenerateDamage();
        GeneratePlayer();
    }

    public void Restart()
    {
        Clear();
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
        Vector3 roadLocalPosition = Vector3.zero;

        for (int i = 0; i < roadLenght; i++)
        {
            GameObject roadPart = Instantiate(roadPartPrefab, transform);
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

            float noDamagePosX = 0;
            if (damagePosition > 0)
            {
                 noDamagePosX = -startPosX + damageOffsetX * damagePosition - damageOffsetX;
            } 
            if (damagePosition < 2)
            {
                noDamagePosX = -startPosX + damageOffsetX * damagePosition + damageOffsetX;
            } 
            
            GameObject noDamage = Instantiate(noDamagePrefab, transform);
            Vector3 localPositionNoDamage = Vector3.zero;
            localPositionNoDamage.x = noDamagePosX;
            localPositionNoDamage.z = currentLength;
            noDamage.transform.localPosition = localPositionNoDamage;
        }
    }

    private void GeneratePlayer()
    {
        GameObject player = Instantiate(playerPrefab, transform);
        player.transform.localPosition = new Vector3(0, 0, roadPartLenght * 0.5f);

        Player = player.GetComponent<PlayerController>();
    }
}

