using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private int roadLenght = 15;
    [SerializeField] private float minDamageOffset = 2f;
    [SerializeField] private float maxDamageOffset = 5f;
    
    [SerializeField] private float roadPartLenght = 5f;
    [SerializeField] private float roadPartWidth = 6f;

    [SerializeField] private GameObject roadPartPrefab;
    [SerializeField] private GameObject damagePrefab;
    [SerializeField] private GameObject finishPrefab;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject destrWallPrefab;

    //private PlayerController _player;
    //public PlayerController Player => _player;
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
        float fullLenght = roadLenght * roadPartLenght;
        float currentLenght = roadPartLenght * 2;
        float damageOffsetX = roadPartWidth / 3;
        float startPosX = roadPartWidth * 0.5f;

        while (currentLenght<fullLenght)
        {
            float zOffset = Random.Range(minDamageOffset, maxDamageOffset) + minDamageOffset;
            currentLenght += zOffset;
            currentLenght = Mathf.Clamp(currentLenght, 0f, fullLenght);

            int damagePosition = Random.Range(0, 3);
            
            InstantiateWall(damagePrefab, damagePosition, startPosX, damageOffsetX, currentLenght);
            if (damagePosition==0)
            {
                damagePosition = 1;
                InstantiateWall(destrWallPrefab, damagePosition, startPosX, damageOffsetX, currentLenght);
                damagePosition = 2;
                InstantiateWall(destrWallPrefab, damagePosition, startPosX, damageOffsetX, currentLenght);
            }
            else if (damagePosition==1)
            {
                damagePosition = 0;
                InstantiateWall(destrWallPrefab, damagePosition, startPosX, damageOffsetX, currentLenght);
                damagePosition = 2;
                InstantiateWall(destrWallPrefab, damagePosition, startPosX, damageOffsetX, currentLenght);
            }
            else
            {
                damagePosition = 0;
                InstantiateWall(destrWallPrefab, damagePosition, startPosX, damageOffsetX, currentLenght);
                damagePosition = 1;
                InstantiateWall(destrWallPrefab, damagePosition, startPosX, damageOffsetX, currentLenght);
            }
        }
    }

    public void GeneratePlayer()
    {
        if (GameObject.FindWithTag("Player") != null)
        {
            Destroy(GameObject.FindWithTag("Player"));
        }
        GameObject player = Instantiate(playerPrefab, transform);
        player.transform.localPosition = new Vector3(0, 0, roadPartLenght * 0.5f);
        Player = player.GetComponent<PlayerController>();
    }

    private void InstantiateWall(GameObject wall, int pos, float startPosX, float damageOffsetX, float currentLenght)
    {
        float damagePosX = -startPosX + damageOffsetX * pos;
        wall = Instantiate(wall, transform);
        Vector3 localPosition = Vector3.zero;
        localPosition.x = damagePosX;
        localPosition.z = currentLenght;
        wall.transform.localPosition = localPosition;
    }

    public void ReInstatiateDestrWalls()
    {
        int count = transform.childCount;
        for (int i = 0; i < count; i++)
        {
            if (transform.GetChild(i).gameObject.CompareTag("DestroyableWall"))
            {
                GameObject child = transform.GetChild(i).gameObject;
                Vector3 position = child.transform.localPosition;
                Destroy(child);
                GameObject newWall = Instantiate(destrWallPrefab, transform);
                newWall.transform.localPosition = position;
            }
        }
    }
}
