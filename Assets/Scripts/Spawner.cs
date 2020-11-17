using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using static Pooler;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public Transform playerPosition;
    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private GameObject _speedUpPrefab;
    [SerializeField] private GameObject _enemyDronePrefab;
    [SerializeField] private GameObject[] _birds;
    [SerializeField] private float _coinPositionToSpawn;
    [SerializeField] private float _speedUpPositionToSpawn;
    [SerializeField] private float _dronePositionToSpawn;
    [SerializeField] private float _birdPositionToSpawn;
    
    //currentPos + thisThresholdPos to spawn object
    private const float AdditionalYPosSpawn = 15;
    [SerializeField] private float _speedUpBoostSpawned = 0;

    private readonly float _xBound = 3.5f;

    private float _coinRandomSpawnYValue;
    
    private float _speedUpRandomSpawnYValue;
    private float _minSpeedUpSpawnRange = 0.7f;
    private float _maxSpeedUpSpawnRange = 1f;

    private void Start()
    {
        InvokeRepeating(nameof(RandomForSpeedUpSpawn), 0, 1f);
    }

    private void Update()
    {
        if(!LevelManager.instance.isPlaying) return;
        CheckForSpawn(_speedUpPrefab, out _speedUpPositionToSpawn,_speedUpPositionToSpawn, 
            1, AdditionalYPosSpawn);
        CheckForSpawn(_coinPrefab, out _coinPositionToSpawn,_coinPositionToSpawn, 
            _coinRandomSpawnYValue, AdditionalYPosSpawn, false);
        CheckForSpawn(_enemyDronePrefab, out _dronePositionToSpawn,_dronePositionToSpawn, 
            1, 100, false);
        CheckForSpawn(_birds[Random.Range(0,_birds.Length)], out _birdPositionToSpawn,_birdPositionToSpawn, 
            1, 50, false);
    }

    private void SpawnObject(GameObject objectToSpawn, float spawnPosition, float addSpawnThreshold)
    {
        var randomXPosition = Random.Range(-_xBound, _xBound);
        var yPosition = spawnPosition + addSpawnThreshold;
        var objectSpawnPosition = new Vector3(randomXPosition, yPosition, 1);

        var instance = Spawn(objectToSpawn, objectSpawnPosition, quaternion.identity);
        var movement = instance.GetComponent<FlyingEnemyMovement>();
        if (movement != null)
        {
            movement.enemySpeed += _speedUpBoostSpawned / 10;
            _speedUpBoostSpawned-=.45f;
        }
    }

    private void CheckForSpawn(GameObject prefab, out float newSpawnPosition, float oldSpawnPosition, float limit, float addSpawnThreshold, bool isStaticSpawnRange = true)
    {
        if (playerPosition.position.y / oldSpawnPosition > limit)//.7f
        {
            if (isStaticSpawnRange)
            {
                newSpawnPosition = oldSpawnPosition + addSpawnThreshold + _speedUpBoostSpawned;
                _speedUpBoostSpawned+=.5f;
            } 
            else
                newSpawnPosition = oldSpawnPosition + Random.Range(0, addSpawnThreshold);

            SpawnObject(prefab,oldSpawnPosition,addSpawnThreshold);
        }
        else
        {
            newSpawnPosition = oldSpawnPosition;
        }
    }

    private void RandomForSpeedUpSpawn()
    {
        _coinRandomSpawnYValue = Random.Range(_minSpeedUpSpawnRange, _maxSpeedUpSpawnRange);
    }

    private void OnEnable()
    {
        playerPosition = LevelManager.instance.currentPlayer.transform;
        
        _coinPositionToSpawn = 0;
        _speedUpPositionToSpawn = 0;
        _dronePositionToSpawn = 0;
        _birdPositionToSpawn = 0;
        
        DestroyPools();
    }
}
