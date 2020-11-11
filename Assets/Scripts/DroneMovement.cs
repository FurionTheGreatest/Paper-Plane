using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneMovement : MonoBehaviour
{
    private Transform _dronePosition;
    [SerializeField] private float droneSpeed = 2;

    private void Start()
    {
        _dronePosition = transform;
    }

    private void Update()
    {
        _dronePosition.position += transform.TransformDirection(Vector3.down * (Time.deltaTime * droneSpeed));
    }
}
