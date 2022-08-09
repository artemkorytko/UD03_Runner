using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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
    private List<NonDamageWall> _savedObjects;

    private void Awake()
    {
        _savedObjects = new List<NonDamageWall>();
    }

    public void GenerateLevel()
    {
        _savedObjects.Clear();
        ClearLevel();
        GenerateRoad();
        GenerateObstacles();
        GeneratePlayer();
    }

    public void Restart()
    {
        RebuildWall();
        Player.transform.localPosition = new Vector3(0, 0, roadPartLenght * 0.5f);
        Player.SetIdle();
    }

    private void RebuildWall()
    {
        foreach (var savedObject in _savedObjects)
        {
            savedObject.OnWall();
        }
    }
    
    private void ClearLevel()
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

    private void GenerateObstacles()
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
            
            if (damagePosition == 0)
            {
                GenerateNoDamage(localPosition, damageOffsetX);
                GenerateNoDamage(localPosition, damageOffsetX * 2);
            } 
            if (damagePosition == 1)
            {
                GenerateNoDamage(localPosition, damageOffsetX);
                GenerateNoDamage(localPosition, -damageOffsetX);
            } 
            if (damagePosition == 2)
            {
                GenerateNoDamage(localPosition, -damageOffsetX);
                GenerateNoDamage(localPosition, -damageOffsetX * 2);
            }
        }
    }

    private void GenerateNoDamage(Vector3 damagePosition,float offset)
    {
        GameObject noDamage = Instantiate(noDamagePrefab, transform);
        _savedObjects.Add(noDamage.GetComponentInChildren<NonDamageWall>());
        damagePosition.x += offset;
        noDamage.transform.localPosition = damagePosition;
    }
    
    private void GeneratePlayer()
    {
        GameObject player = Instantiate(playerPrefab, transform);
        player.transform.localPosition = new Vector3(0, 0, roadPartLenght * 0.5f);

        Player = player.GetComponent<PlayerController>();
    }
}

