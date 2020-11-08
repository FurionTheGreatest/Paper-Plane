using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5;
    [SerializeField] private float forceValue = 10;
    private Transform _planePosition;
    private Rigidbody2D _rigidbody2D;

    private void Start()
    {
        _planePosition = transform;
        _rigidbody2D = GetComponent<Rigidbody2D>();
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Force();
        }
    }

    private void Force()
    {
        _rigidbody2D.AddForce(Vector2.up * forceValue, ForceMode2D.Impulse);
        StartCoroutine(ResetVelocity());
    }

    private IEnumerator ResetVelocity()
    {
        yield return  new WaitForSeconds(1f);
        _rigidbody2D.velocity = Vector2.zero;
    }
}
