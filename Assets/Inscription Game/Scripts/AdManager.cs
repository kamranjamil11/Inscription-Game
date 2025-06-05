using UnityEngine;
using GoogleMobileAds.Api;
using System;
using System.Drawing;
using UnityEngine.UIElements;

public class AdManager : MonoBehaviour
{
    public static AdManager Instance;

    private BannerView bannerView;
    private InterstitialAd interstitialAd;
    private RewardedAd rewardedAd;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        MobileAds.Initialize(initStatus =>
        {
            Debug.Log("AdMob Initialized");
        });

       // RequestBanner();
        // RequestInterstitial();
        // RequestRewarded();
        LoadInterstitialAd();
    }

    #region Banner Ads

    public void RequestBanner()
    {
        string bannerAdUnitId = "ca-app-pub-3940256099942544/6300978111"; // Test ID

#if UNITY_ANDROID
        bannerAdUnitId = "ca-app-pub-3940256099942544/6300978111"; // Replace with your actual ID
#elif UNITY_IOS
            bannerAdUnitId = "ca-app-pub-3940256099942544/2934735716"; // Replace with your actual ID
#endif

        bannerView = new BannerView(bannerAdUnitId, AdSize.Banner, AdPosition.Bottom);
        AdRequest request = new AdRequest.Builder().Build();
        bannerView.LoadAd(request);
    }
    public void ShowBanner()
    {
        if (!SettingPopup.isPortrait)
        {
            if (bannerView == null)
            {
                RequestBanner();
            }
            else
            {
                bannerView.Show();
            }
        }
    }

    public void HideBanner()
    {
        if (bannerView != null)
        {
            bannerView.Hide();
        }
    }


    #endregion

    #region Interstitial Ads
    private InterstitialAd interstitial;
    public GameController gm_Controller;
    public void LoadInterstitialAd()
    {
        
        string interstitialAdUnitId = "ca-app-pub-3940256099942544/1033173712"; // Test ID

#if UNITY_ANDROID
        interstitialAdUnitId = "ca-app-pub-3940256099942544/1033173712"; // Replace with your actual ID
#elif UNITY_IOS
                interstitialAdUnitId = "ca-app-pub-3940256099942544/4411468910"; // Replace with your actual ID
#endif

        AdRequest adRequest = new AdRequest();
        // Add event handlers
        
        InterstitialAd.Load(interstitialAdUnitId, adRequest,
            (InterstitialAd ad, LoadAdError error) =>
            {
                if (error != null || ad == null)
                {
                    Debug.LogError("Failed to load interstitial ad: " + error);
                    return;
                }
                

                interstitial = ad;
               
                Debug.Log("Interstitial ad loaded.");
               // Set FullScreenContentCallback events
                interstitial.OnAdFullScreenContentClosed += () =>
                {
                    Debug.Log("Interstitial ad closed.");
                    LoadInterstitialAd(); // Load the next one
                    gm_Controller = GameObject.FindObjectOfType<GameController>();
                    gm_Controller.ShowLoading();
                };

                interstitial.OnAdFullScreenContentFailed += (AdError adError) =>
                {
                    Debug.LogError("Interstitial ad failed to show: " + adError);
                    gm_Controller = GameObject.FindObjectOfType<GameController>();
                    gm_Controller.ShowLoading();
                };

                interstitial.OnAdFullScreenContentOpened += () =>
                {
                    Debug.Log("Interstitial ad opened.");
                   // gm_Controller.ShowLoading();
                };
            });

    }

    public void ShowInterstitialAd()
    {
        if (!SettingPopup.isPortrait)
        {
            if (!PlayerPrefs.HasKey("NO_ADS"))
            {
                if (Application.internetReachability != NetworkReachability.NotReachable)
                {
                    if (interstitial != null && interstitial.CanShowAd())
                    {
                        interstitial.Show();

                    }
                }
                else
                {
                    gm_Controller = GameObject.FindObjectOfType<GameController>();
                    gm_Controller.ShowLoading();
                }
            }
        }
        else 
        {
            gm_Controller = GameObject.FindObjectOfType<GameController>();
            gm_Controller.ShowLoading();
        }
    }
    


    //    public void RequestInterstitial()
    //    {
    //        string interstitialAdUnitId = "ca-app-pub-3940256099942544/1033173712"; // Test ID

    //#if UNITY_ANDROID
    //        interstitialAdUnitId = "ca-app-pub-3940256099942544/1033173712"; // Replace with your actual ID
    //#elif UNITY_IOS
    //        interstitialAdUnitId = "ca-app-pub-3940256099942544/4411468910"; // Replace with your actual ID
    //#endif

    //        interstitialAd = new InterstitialAd(interstitialAdUnitId);
    //        AdRequest request = new AdRequest.Builder().Build();
    //        interstitialAd.LoadAd(request);
    //    }

    //    public void ShowInterstitial()
    //    {
    //        if (interstitialAd != null && interstitialAd.IsLoaded())
    //        {
    //            interstitialAd.Show();
    //            RequestInterstitial(); // Load next
    //        }
    //        else
    //        {
    //            Debug.Log("Interstitial Ad not ready");
    //        }
    //    }

    #endregion

    //    #region Rewarded Ads

    //    public void RequestRewarded()
    //    {
    //        string rewardedAdUnitId = "ca-app-pub-3940256099942544/5224354917"; // Test ID

    //#if UNITY_ANDROID
    //        rewardedAdUnitId = "ca-app-pub-3940256099942544/5224354917"; // Replace with your actual ID
    //#elif UNITY_IOS
    //        rewardedAdUnitId = "ca-app-pub-3940256099942544/1712485313"; // Replace with your actual ID
    //#endif

    //        rewardedAd = new RewardedAd(rewardedAdUnitId);
    //        rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
    //        rewardedAd.OnAdClosed += HandleRewardedAdClosed;

    //        AdRequest request = new AdRequest.Builder().Build();
    //        rewardedAd.LoadAd(request);
    //    }

    //    public void ShowRewarded()
    //    {
    //        if (rewardedAd != null && rewardedAd.IsLoaded())
    //        {
    //            rewardedAd.Show();
    //        }
    //        else
    //        {
    //            Debug.Log("Rewarded Ad not ready");
    //        }
    //    }

    //    private void HandleUserEarnedReward(object sender, Reward args)
    //    {
    //        Debug.Log("Rewarded! You earned: " + args.Amount + " " + args.Type);
    //        // Give reward to player here
    //    }

    //    private void HandleRewardedAdClosed(object sender, EventArgs args)
    //    {
    //        RequestRewarded(); // Load next
    //    }

    //    #endregion
}
