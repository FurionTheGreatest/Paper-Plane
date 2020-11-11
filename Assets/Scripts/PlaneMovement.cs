using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMovement : MonoBehaviour
{
    [SerializeField] private float _currentSpeed;
    [SerializeField] private float defaultSpeed = 3;
    [SerializeField] private float forceSpeed = 8;
    private Transform _planePosition;

    private void Start()
    {
        _planePosition = transform;
        _currentSpeed = defaultSpeed;
    }

    private void Update()
    {
        _planePosition.position += transform.TransformDirection(Vector3.up * (Time.deltaTime * _currentSpeed));

        if (Input.GetKey (KeyCode.A) && !Input.GetKey (KeyCode.D) && transform.position.x > -4f)
        {
            _planePosition.position += transform.TransformDirection (Vector3.left) * (Time.deltaTime * defaultSpeed);
        } 
        else if (Input.GetKey (KeyCode.D) && !Input.GetKey (KeyCode.A) && transform.position.x < 4f)
        {
            _planePosition.position -= transform.TransformDirection (Vector3.left) * (Time.deltaTime * defaultSpeed);
        }
    }
    
    private void Force()
    {
        _currentSpeed = forceSpeed;
        StartCoroutine(ResetVelocity());
    }
    private IEnumerator ResetVelocity()
    {
        yield return  new WaitForSeconds(1f);
        _currentSpeed = defaultSpeed;
    }
    
    private void OnEnable()
    {
        SpeedUp.OnSpeedUpCollect += Force;
    }

    private void OnDisable()
    {
        SpeedUp.OnSpeedUpCollect -= Force;
    }
}
