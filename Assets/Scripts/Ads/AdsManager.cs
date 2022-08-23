using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Ads;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] string _androidGameId;
    [SerializeField] string _iOSGameId;
    [SerializeField] bool _testMode = true;
    private string _gameId;
    private InterstitialAdExample _interstitialAdExample;
    private RewardedAdsButton _rewardedAdsButton;

    void Awake()
    {
        InitializeAds();
        _interstitialAdExample = GetComponent<InterstitialAdExample>();
        _rewardedAdsButton = GetComponent<RewardedAdsButton>();
    }
 
    public void InitializeAds()
    {
        _gameId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOSGameId
            : _androidGameId;
        Advertisement.Initialize(_gameId, _testMode, this);
    }
 
    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
        _interstitialAdExample.LoadAd();
        _rewardedAdsButton.LoadAd();
    }
 
    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }

    private void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     _interstitialAdExample.ShowAd();
        // }
    }
}
