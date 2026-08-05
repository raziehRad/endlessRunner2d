using System;
using Unity.Services.Core;
using Unity.Services.Mediation;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour , IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
#if UNITY_ANDROID
    private string gameId="5972337"; 
#elif UNITY_IOS
    private string gameId="5972336"; 
#else 
    private string gameId="5972337"; 
#endif
    private string adUnitId = "Interstitial_Android";
    private bool testMode = true;
    public bool isAdReady = false;
    
    private string rewardedAdUnitId = "Rewarded_Android";
    private bool isRewardedAdReady = false;

    private int _reward=100;
    private void Start()
    {
        Advertisement.Initialize(gameId,testMode,this);
    }
    
    public void ShowAd()
    {
        Debug.Log("trying to show ad: "+adUnitId);
        if (isAdReady)
        {
            Advertisement.Show(adUnitId,this);
            isAdReady = false;
        }
        else
        {
            Debug.Log("ad not ready yes!");
        }
    }

    public void OnInitializationComplete()
    {
        Debug.Log("unity ads initialization complete");
        LoadInterstitialAd();
        LoadRewardedAd();
    }
    public void LoadInterstitialAd()
    {
        Debug.Log("Loading interstitial ad: " + adUnitId);
        Advertisement.Load(adUnitId, this);
    }

    public void LoadRewardedAd()
    {
        Debug.Log("Loading rewarded ad: " + rewardedAdUnitId);
        Advertisement.Load(rewardedAdUnitId, this);
    }
    public void ShowRewardedAd()
    {
        if (isRewardedAdReady)
        {
            Debug.Log("Showing rewarded ad: " + rewardedAdUnitId);
            Advertisement.Show(rewardedAdUnitId, this);
            isRewardedAdReady = false;
        }
        else
        {
            Debug.Log("Rewarded ad not ready yet!");
        }
    }
    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"unity ads initialization failed:{error.ToString()} - {message}");
    }
    //callback for loading ads
    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log("Ad Loaded: "+placementId );
        if (placementId.Equals(adUnitId))
            isAdReady = true;
        else if (placementId.Equals(rewardedAdUnitId))
            isRewardedAdReady = true;
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Failed to load ad unit {placementId}:{error.ToString()} - {message}");
    }
    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing ad unit {placementId}:{error.ToString()} - {message}");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        Debug.Log("ad started showing: "+placementId);
    }
    public void OnUnityAdsShowClick(string placementId)
    {
        Debug.Log("ad click: "+placementId);
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log($"ad finished: {showCompletionState}");
       
        if (placementId.Equals(adUnitId))
        {
            LoadInterstitialAd(); // بعد از دیدن مجدد لود میشه
        }
        else if (placementId.Equals(rewardedAdUnitId))
        {
            if (showCompletionState == UnityAdsShowCompletionState.COMPLETED)
            {
                Debug.Log("Reward the player!");
                GiveRewardToPlayer();
            }

            LoadRewardedAd(); // مجدداً لود کن
        }
    }
    private void GiveRewardToPlayer()
    {
        // پاداش به بازیکن بده (مثلاً سکه، جان، انرژی و غیره)
        GameEvents.OnScoreChanged?.Invoke(_reward);
        //HUDManager.Instance.AddScore(_reward);
        Debug.Log("Player rewarded!");
    }
}
