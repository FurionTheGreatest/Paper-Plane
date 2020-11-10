using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanePhysics : MonoBehaviour
{
    [SerializeField] private float forceValue = 10;
    private Rigidbody2D _rigidbody2D;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
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
    
    private void OnEnable()
    {
        SpeedUp.OnSpeedUpCollect += Force;
    }

    private void OnDisable()
    {
        SpeedUp.OnSpeedUpCollect -= Force;
    }
}
