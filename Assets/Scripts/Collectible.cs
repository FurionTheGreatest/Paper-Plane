using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Collectible : MonoBehaviour
{
    public UnityEvent onCollect;
    private const string PlayerTag = "Player";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.tag.Equals(PlayerTag)) return;
        onCollect?.Invoke();
        
        Destroy(gameObject);
    }
}
