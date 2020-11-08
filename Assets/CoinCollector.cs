﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    private TMP_Text _coinCountText;
    [SerializeField] private int _coinCount;

    private void Awake()
    {
        _coinCountText = GetComponent<TMP_Text>();
        _coinCount = 0;
    }

    private void CollectCoin()
    {
        _coinCount++;
        _coinCountText.text = _coinCount.ToString();
    }

    private void OnEnable()
    {
        Collectible.OnCoinCollect += CollectCoin;
    }

    private void OnDisable()
    {
        Collectible.OnCoinCollect -= CollectCoin;
    }
}
