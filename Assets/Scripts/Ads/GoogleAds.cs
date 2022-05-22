using UnityEngine;
using GoogleMobileAds.Api;

public class GoogleAds : MonoBehaviour
{
    public static GoogleAds Instance { get; private set; }

    private BannerView bannerView;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        MobileAds.Initialize(initStatus => { });
    }

    public void ShowBanner()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-6502722460790788~6184484374";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
            string adUnitId = "unexpected_platform";
#endif

        if (bannerView != null)
            bannerView.Destroy();

        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);

        AdRequest request = new AdRequest.Builder().Build();

        bannerView.LoadAd(request);
    }
}