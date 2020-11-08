using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public Transform playerPosition;
    [SerializeField] private GameObject _windyObject;
    [SerializeField] private int _positionToSpawn;
    private const int SpawnThreshold = 15;

    private readonly float _xBound = 3.5f;


    private void Start()
    {
        _positionToSpawn = SpawnThreshold;
    }

    private void Update()
    {
        Debug.Log(playerPosition.position.y / _positionToSpawn);
        
        if (playerPosition.position.y / _positionToSpawn < 1) return;
        SpawnObject(_windyObject);
        _positionToSpawn += SpawnThreshold;
    }

    private void SpawnObject(GameObject objectToSpawn)
    {
        var randomXPosition = Random.Range(-_xBound, _xBound);
        var yPosition = _positionToSpawn + SpawnThreshold;
        var spawnPosition = new Vector3(randomXPosition,yPosition);
        var instantiatedGo = Instantiate(objectToSpawn, spawnPosition, quaternion.identity);
    }
}
