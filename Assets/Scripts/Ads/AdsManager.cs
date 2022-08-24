using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] string _androidGameId;
    [SerializeField] string _iOSGameId;
    [SerializeField] bool _testMode = true;
    private string _gameId;
    private InterstitialAdExample _interstitialAdExample;
   
    void Awake()
    {
        InitializeAds();
        _interstitialAdExample = GetComponent<InterstitialAdExample>();
    }

    public void InitializeAds()
    {
        _gameId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOSGameId
            : _androidGameId;
        Advertisement.Initialize(_gameId, _testMode, this);
        Debug.Log("Unity Ads initialization start.");

    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
        _interstitialAdExample.LoadAd();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _interstitialAdExample.ShowAd();
        }
    }
}