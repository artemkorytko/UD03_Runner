using System.Collections.Generic;
using Configs;
using UnityEngine;
using Random = UnityEngine.Random;

public enum LevelDifficulty
{
    None,
    Easy,
    Normal,
    Hard
}


public class Level : MonoBehaviour
{
    [SerializeField] private LevelPrefabs levelPrefabs;
    [SerializeField] private LevelConfig[] levelConfigs;

    [SerializeField] private LevelDifficulty difficulty;

    private LevelDifficulty _currentDifficulty;

    private LevelDifficulty Difficulty
    {
        get => _currentDifficulty;
        set
        {
            if (_currentDifficulty == value) 
                return;
            
            _currentDifficulty = value;
            
            foreach (var config in levelConfigs)
            {
                if (config.Difficulty == _currentDifficulty)
                {
                    ReadLevelConfig(config);
                }
            }
        }
    }
    
    public PlayerController Player { get; private set; }
    
    private List<NonDamageWall> _savedObjects;
    private int _roadLenght;
    private float _minDamageOffset;
    private float _maxDamageOffset;
    private float _roadPartLenght;
    private float _roadPartWidth;
    private GameObject _roadPartPrefab;
    private GameObject _damagePrefab;
    private GameObject _noDamagePrefab;
    private GameObject _finishPrefab;
    private GameObject _playerPrefab;

    private void Awake()
    {
        _savedObjects = new List<NonDamageWall>();
        ReadPrefabConfig();
    }

    public void GenerateLevel()
    {
        Difficulty = difficulty;
        _savedObjects.Clear();
        ClearLevel();
        GenerateRoad();
        GenerateObstacles();
        GeneratePlayer();
    }

    public void Restart()
    {
        RebuildWall();
        Player.transform.localPosition = new Vector3(0, 0, _roadPartLenght * 0.5f);
        Player.SetIdle();
    }

    private void ReadPrefabConfig()
    {
        _roadPartPrefab = levelPrefabs.RoadPart;
        _damagePrefab = levelPrefabs.DamageWall;
        _noDamagePrefab = levelPrefabs.NoDamageWall;
        _finishPrefab = levelPrefabs.Finish;
        _playerPrefab = levelPrefabs.Player;
    } 
    private void ReadLevelConfig(LevelConfig config)
    {
        _roadLenght = config.RoadLenght;
        _minDamageOffset = config.MinDamageOffset;
        _maxDamageOffset = config.MaxDamageOffset;
        _roadPartLenght = config.RoadPartLenght;
        _roadPartWidth = config.RoadPartWidth;
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

        for (int i = 0; i < _roadLenght; i++)
        {
            GameObject roadPart = Instantiate(_roadPartPrefab, transform);
            roadPart.transform.localPosition = roadLocalPosition;
            roadLocalPosition.z += _roadPartLenght;
        }

        GameObject finish = Instantiate(_finishPrefab, transform);
        finish.transform.localPosition = roadLocalPosition;
    }

    private void GenerateObstacles()
    {
        float fullLength = _roadLenght * _roadPartLenght;
        float currentLength = _roadPartLenght * 2;
        float damageOffsetX = _roadPartWidth / 3;
        float startPosX = _roadPartWidth * 0.5f;

        while (currentLength < fullLength)
        {
            float zOffset = Random.Range(_minDamageOffset, _maxDamageOffset) + _minDamageOffset;
            currentLength += zOffset;
            currentLength = Mathf.Clamp(currentLength, 0f, fullLength);

            int damagePosition = Random.Range(0, 3);
            float damagePosX = -startPosX + damageOffsetX * damagePosition;

            GameObject damage = Instantiate(_damagePrefab, transform);

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
        GameObject noDamage = Instantiate(_noDamagePrefab, transform);
        _savedObjects.Add(noDamage.GetComponentInChildren<NonDamageWall>());
        damagePosition.x += offset;
        noDamage.transform.localPosition = damagePosition;
    }
    
    private void GeneratePlayer()
    {
        GameObject player = Instantiate(_playerPrefab, transform);
        player.transform.localPosition = new Vector3(0, 0, _roadPartLenght * 0.5f);

        Player = player.GetComponent<PlayerController>();
    }
}

