using System;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;

// Example script showing how to invoke the Google Mobile Ads Unity plugin.
public class ads : MonoBehaviour
{
	public static BannerView bannerView;
	//public static string adUnitId = "ca-app-pub-3940256099942544/6300978111";
	private static int count = 0;
	public static string adUnitId = "ca-app-pub-5079073523461972/6907889713";
	
	void Start ()
	{
		ads.RequestBanner ();
	}

	public static void RequestBanner ()
	{
         
		// Create a 320x50 banner at the top of the screen.
		ads.bannerView = new BannerView (ads.adUnitId, AdSize.Banner, AdPosition.Bottom);
		//bannerView.AdLoaded += HandleAdLoaded;

		// Create an empty ad request.
		AdRequest request = new AdRequest.Builder ().Build ();
		// Load the banner with the request.
		ads.bannerView.LoadAd (request);
		ads.bannerView.Hide ();
	}

	public void HandleAdLoaded (object sender, EventArgs args)
	{
		print ("HandleAdLoaded event received.");
	}

	public static void hideAdss ()
	{
		ads.count += 1;
		if (ads.count % 3 == 0) {
			ads.bannerView.Destroy ();
			ads.RequestBanner ();
		} else {
			ads.bannerView.Hide ();
		}
	}
	 
}