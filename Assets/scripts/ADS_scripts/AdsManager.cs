using UnityEngine;
using UnityEngine.Advertisements;

namespace ADS_scripts
{
    public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener
    {
        [SerializeField] string _androidGameId;
        [SerializeField] string _iOSGameId;
        [SerializeField] bool _testMode = true;
        private string _gameId;
        private RewardedAdsButton _rewardedAdsButton;
 
        void Awake()
        {
            InitializeAds();
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
            _rewardedAdsButton.LoadAd();
        }
 
        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
        }
    }
}