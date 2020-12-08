using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class PlaneMovement : MonoBehaviour
{
    [SerializeField] private float _currentSpeed;
    [SerializeField] private float defaultSpeed = 3;
    [SerializeField] private float sideSpeed = 4;
    [SerializeField] private float forceSpeed = 8;
    private Transform _planePosition;

    private float _minNoFlickValue = 0.1f;

    private void Start()
    {
        _planePosition = transform;
        _currentSpeed = defaultSpeed;
    }

    private void Update()
    {
        _planePosition.position += transform.TransformDirection(Vector3.up * (Time.deltaTime * _currentSpeed));
#if UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetKey (KeyCode.A) && !Input.GetKey (KeyCode.D) && transform.position.x > -4f)
        {
            _planePosition.position += transform.TransformDirection (Vector3.left) * (Time.deltaTime * sideSpeed);
        } 
        else if (Input.GetKey (KeyCode.D) && !Input.GetKey (KeyCode.A) && transform.position.x < 4f)
        {
            _planePosition.position += transform.TransformDirection (Vector3.right) * (Time.deltaTime * sideSpeed);
        }
#endif
        
#if UNITY_ANDROID
        if(!Input.touchSupported) return;
        
        var touchWorldPosition = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
        
        if (Mathf.Abs(touchWorldPosition.x - _planePosition.position.x) < _minNoFlickValue) return;
        
        if ( touchWorldPosition.x <= _planePosition.position.x  && transform.position.x > -4f)
        {
            _planePosition.position += transform.TransformDirection (Vector3.left) * (Time.deltaTime * sideSpeed);
        } 
        else if (touchWorldPosition.x > _planePosition.position.x && transform.position.x < 4f)
        {
            _planePosition.position += transform.TransformDirection (Vector3.right) * (Time.deltaTime * sideSpeed);
        }
#endif
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
