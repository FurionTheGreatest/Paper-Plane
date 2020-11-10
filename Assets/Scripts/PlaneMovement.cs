using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5;
    private Transform _planePosition;

    private void Start()
    {
        _planePosition = transform;
    }

    private void Update()
    {
        _planePosition.position += transform.TransformDirection(Vector3.up * (Time.deltaTime * movementSpeed));

        if (Input.GetKey (KeyCode.A) && !Input.GetKey (KeyCode.D) && transform.position.x > -4f)
        {
            _planePosition.position += transform.TransformDirection (Vector3.left) * (Time.deltaTime * movementSpeed);
        } 
        else if (Input.GetKey (KeyCode.D) && !Input.GetKey (KeyCode.A) && transform.position.x < 4f)
        {
            _planePosition.position -= transform.TransformDirection (Vector3.left) * (Time.deltaTime * movementSpeed);
        }
    }
}
