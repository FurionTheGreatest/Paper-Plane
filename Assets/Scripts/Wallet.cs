using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.GameFoundation;
using UnityEngine.GameFoundation.DefaultLayers;
using UnityEngine.GameFoundation.DefaultLayers.Persistence;
using UnityEngine.GameFoundation.Sample;


public class Wallet : MonoBehaviour
{
    public const int k_InitialQuantity = 50;
    public TMP_Text coinUiValue;
    private const string CoinName = "coin";
    
    private bool m_SubscribedFlag = false;
    private bool m_WalletChanged;

    private Currency m_CoinDefinition;
    private PersistenceDataLayer _persistenceDataLayer;

    private IEnumerator Start()
    {
        _persistenceDataLayer = new PersistenceDataLayer(
            new LocalPersistence("DataPersistencePaperPlane", new JsonDataSerializer()));

        // Initialize Game Foundation.
        yield return InitializeGameFoundation();
    }
    private void Update()
    {
        // This flag will be set to true when the balance of a currency has changed in the WalletManager
        if (m_WalletChanged)
        {
            RefreshUI();
            m_WalletChanged = false;
        }
    }

    public IEnumerator InitializeGameFoundation()
    {
        var initDeferred = GameFoundationSdk.Initialize(_persistenceDataLayer);

        // Wait for initialization to complete, then continue
        yield return initDeferred.Wait();

        if (initDeferred.isFulfilled)
        {
            OnGameFoundationInitialized();
        }
        else
        {
            OnGameFoundationException(initDeferred.error);
        }

        // Release deferred promise handler.
        initDeferred.Release();
    }

    public void AddCurrency(int value)
    {
        bool success = GameFoundationSdk.wallet.Add(m_CoinDefinition, value);

        if (!success)
        {
            Debug.LogError($"Failed in setting a new value for '{m_CoinDefinition.displayName}'");
        }
    }
    
    public void RemoveCurrency(int value)
    {
        bool success = GameFoundationSdk.wallet.Remove(m_CoinDefinition, value);

        if (!success)
        {
            Debug.LogError($"Failed in setting a new value for '{m_CoinDefinition.displayName}'");
        }
    }
    
    private void OnGameFoundationInitialized()
    {
        // We'll initialize our WalletManager's coin balance with correct quantity.
        // This will set the correct balance no matter what it's current balance is.
        m_CoinDefinition = GameFoundationSdk.catalog.Find<Currency>(CoinName);
        
        GameFoundationSdk.wallet.Set(m_CoinDefinition, m_CoinDefinition.quantity);
    

        // Show list of currencies and update the button interactability.
        RefreshUI();

        // Ensure that the wallet changed callback is connected
        SubscribeToGameFoundationEvents();
    }
    
    private void OnGameFoundationException(Exception exception)
    {
        Debug.LogError($"GameFoundation exception: {exception}");
    }
    private void RefreshUI()
    {
        var coinBalance = GameFoundationSdk.wallet.Get(m_CoinDefinition);

        coinUiValue.text = coinBalance.ToString();

        _persistenceDataLayer.Save();
    }
    
    private void SubscribeToGameFoundationEvents()
    {
        // If wallet has not yet been initialized the ignore request.
        if (GameFoundationSdk.wallet is null)
        {
            return;
        }

        // If app has been disabled then ignore the request
        if (!enabled)
        {
            return;
        }

        // If balanceChanged callback has not been added then add it now and remember.
        // Note: this ignores repeated requests to add callback if already properly set up.
        if (!m_SubscribedFlag)
        {
            GameFoundationSdk.wallet.balanceChanged += OnCoinBalanceChanged;
            m_SubscribedFlag = true;
        }
    }
    private void OnCoinBalanceChanged(IQuantifiable quantifiable, long _)
    {
        if (quantifiable is Currency currency)
        {
            if (currency.key == m_CoinDefinition.key)
            {
                m_WalletChanged = true;
            }
        }
    }

}
