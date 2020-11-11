using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public Transform playerPosition;
    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private GameObject _speedUpPrefab;
    [SerializeField] private float _coinPositionToSpawn;
    [SerializeField] private float _speedUpPositionToSpawn;
    
    //currentPos + thisThresholdPos to spawn object
    private const int AdditionalYPosSpawn = 15;


    private readonly float _xBound = 3.5f;

    private float _coinRandomSpawnYValue = .1f;
    
    private float _speedUpRandomSpawnYValue;
    private float _minSpeedUpSpawnRange = 0.3f;
    private float _maxSpeedUpSpawnRange = 1f;

    private void Start()
    {
        _coinPositionToSpawn = AdditionalYPosSpawn;
        _speedUpPositionToSpawn = AdditionalYPosSpawn;
        InvokeRepeating(nameof(RandomForSpeedUpSpawn), 0, 1f);
    }

    private void Update()
    {
        CheckForSpawn(_speedUpPrefab, out _speedUpPositionToSpawn,_speedUpPositionToSpawn, 
            1, AdditionalYPosSpawn);
        CheckForSpawn(_coinPrefab, out _coinPositionToSpawn,_coinPositionToSpawn, 
            _coinRandomSpawnYValue, AdditionalYPosSpawn/2);
    }

    private void SpawnObject(GameObject objectToSpawn, float spawnPosition, int addSpawnThreshold)
    {
        var randomXPosition = Random.Range(-_xBound, _xBound);
        var yPosition = spawnPosition + addSpawnThreshold;
        var objectSpawnPosition = new Vector3(randomXPosition, yPosition, 1);
        var instantiatedGo = Instantiate(objectToSpawn, objectSpawnPosition, quaternion.identity, gameObject.transform);
    }

    private void CheckForSpawn(GameObject prefab, out float newSpawnPosition, float oldSpawnPosition, float limit, int addSpawnThreshold)
    {
        if (!(playerPosition.position.y / oldSpawnPosition > limit))
        {
            newSpawnPosition = oldSpawnPosition;
            return;
        }
        SpawnObject(prefab, oldSpawnPosition, addSpawnThreshold);
        newSpawnPosition = oldSpawnPosition + AdditionalYPosSpawn;
    }

    private void RandomForSpeedUpSpawn()
    {
        _coinRandomSpawnYValue = Random.Range(_minSpeedUpSpawnRange, _maxSpeedUpSpawnRange);
    }
}
