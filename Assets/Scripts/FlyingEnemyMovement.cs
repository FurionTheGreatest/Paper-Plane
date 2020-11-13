using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyMovement : MonoBehaviour
{
    private Transform _dronePosition;
    [SerializeField] private float enemySpeed = 2;

    private void Start()
    {
        _dronePosition = transform;
    }

    private void Update()
    {
        _dronePosition.position += transform.TransformDirection(Vector3.down * (Time.deltaTime * enemySpeed));
    }
}
