using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public static Action OnCoinCollect;
    private const string PlayerTag = "Player";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.tag.Equals(PlayerTag)) return;
        OnCoinCollect?.Invoke();
        
        Destroy(gameObject);
    }
}
