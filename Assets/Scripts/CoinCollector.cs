using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    private Wallet _wallet;

    private void Awake()
    {
        _wallet = FindObjectOfType<Wallet>();
    }

    private void CollectCoin()
    {
        _wallet.AddCurrency(1);
    }

    private void OnEnable()
    {
        CoinPickUp.OnCoinCollect += CollectCoin;
    }

    private void OnDisable()
    {
        CoinPickUp.OnCoinCollect -= CollectCoin;
    }
}
