using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    public static Action OnCoinCollect;

    public void CollectCoin()
    {
        OnCoinCollect?.Invoke();
    }
}
