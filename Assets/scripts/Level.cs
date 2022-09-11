using System;
using System.Collections;
using System.Collections.Generic;
using Level_configs;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Random = UnityEngine.Random;
using Task = System.Threading.Tasks.Task;


public enum Difficults
{
    None,
    Easy,
    Normal,
    Hard
}
public class Level : MonoBehaviour
{

    [SerializeField] private Difficults difficults;
    [SerializeField] private AssetReference[] difficultConfigsReferences;
    [SerializeField] private AssetReference damagePrefabReference;
    [SerializeField] private AssetReference playerPrefabReference;
    [SerializeField] private AssetReference destrWallPrefabReference;

    private GameObject _damagePrefab;
    private GameObject _playerPrefab;
    private GameObject _destrWallPrefab;

    private Base[] _difficultConfigs;
    private Base _currentDifficult;

    public PlayerController Player { get; private set; }
    

    private Difficults Difficults
    {
        get => difficults;
        set
        {
            difficults = value;
            foreach (Base difConfig in _difficultConfigs)
            {
                if (difConfig.Difficult == difficults)
                {
                    _currentDifficult = difConfig;
                    break;
                }
            }
        }
    }
    

    public async void GenerateLevel()
    {
        await GetReferences();
        Difficults = difficults;
        Clear();
        GenerateRoad();
        GenerateDamage();
        GeneratePlayer();
    }

    private async Task GetReferences()
    {
        _difficultConfigs = new Base[difficultConfigsReferences.Length];
        for (int i = 0; i < _difficultConfigs.Length; i++)
        {
            AsyncOperationHandle<Base> handle = difficultConfigsReferences[i].LoadAssetAsync<Base>();
            await handle.Task;
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                _difficultConfigs[i] = handle.Result;
                Addressables.Release(handle);
            }
        }

        AsyncOperationHandle<GameObject> damPref = damagePrefabReference.LoadAssetAsync<GameObject>();
        await damPref.Task;
        if (damPref.Status == AsyncOperationStatus.Succeeded)
        {
            _damagePrefab = damPref.Result;
            Addressables.Release(damPref);
        }
        
        AsyncOperationHandle<GameObject> playerPref = playerPrefabReference.LoadAssetAsync<GameObject>();
        await playerPref.Task;
        if (playerPref.Status == AsyncOperationStatus.Succeeded)
        {
            _playerPrefab = playerPref.Result;
            Addressables.Release(playerPref);
        }
        
        AsyncOperationHandle<GameObject> destrWallPref = destrWallPrefabReference.LoadAssetAsync<GameObject>();
        await destrWallPref.Task;
        if (destrWallPref.Status == AsyncOperationStatus.Succeeded)
        {
            _destrWallPrefab = destrWallPref.Result;
            Addressables.Release(destrWallPref);
        }
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

        for (int i = 0; i < _currentDifficult.RoadLenght; i++)
        {
            GameObject roadPart = Instantiate(_currentDifficult.RoadPartPrefab, transform);
            roadPart.transform.localPosition = roadLocalPosition;
            roadLocalPosition.z += _currentDifficult.RoadPartLenght;
        }

        GameObject finish = Instantiate(_currentDifficult.FinishPrefab, transform);
        finish.transform.localPosition = roadLocalPosition;
    }

    private void GenerateDamage()
    {
        float fullLenght = _currentDifficult.RoadLenght * _currentDifficult.RoadPartLenght;
        float currentLenght = _currentDifficult.RoadPartLenght * 2;
        float damageOffsetX = _currentDifficult.RoadPartWidth / 3;
        float startPosX = _currentDifficult.RoadPartWidth * 0.5f;

        while (currentLenght<fullLenght)
        {
            float zOffset = Random.Range(_currentDifficult.MinDamageOffset, _currentDifficult.MaxDamageOffset) + _currentDifficult.MinDamageOffset;
            currentLenght += zOffset;
            currentLenght = Mathf.Clamp(currentLenght, 0f, fullLenght);

            int damagePosition = Random.Range(0, _currentDifficult.CountOfWallsInLine);
            InstantiateWall(_damagePrefab, damagePosition, startPosX, damageOffsetX, currentLenght);

            List<int> positions = new List<int>();
            for (int i = 0; i < _currentDifficult.CountOfWallsInLine; i++)
            {
                if (i!=damagePosition)
                {
                    positions.Add(i);
                }
            }
            
            foreach (int pos in positions)
            {
                InstantiateWall(_destrWallPrefab, pos, startPosX, damageOffsetX, currentLenght);
            }
        }
    }

    public void GeneratePlayer()
    {
        if (Player != null)
        {
            Destroy(Player.gameObject);
        }
        GameObject player = Instantiate(_playerPrefab, transform);
        player.transform.localPosition = new Vector3(0, 0, _currentDifficult.RoadPartLenght * 0.5f);
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
        int countOfChildrens = transform.childCount;
        List<GameObject> listOfChildren = new List<GameObject>();
        
        for (int i = 0; i < countOfChildrens; i++)
        {
            if (transform.GetChild(i).GetChild(0).GetComponent<DestroyableWall>())
            {
                listOfChildren.Add(transform.GetChild(i).gameObject);
            }
        }

        foreach (GameObject wall in listOfChildren)
        {
            Vector3 position = wall.transform.localPosition;
            Destroy(wall);
            GameObject newWall = Instantiate(_destrWallPrefab, transform);
            newWall.transform.localPosition = position;
        }
    }
}
