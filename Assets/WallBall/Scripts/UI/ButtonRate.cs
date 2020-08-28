using UnityEngine;
using System.Collections;

// This script is attached to the "Rate" Button in the main menu
// It opens up google play store or itunes, if clicked

namespace GemMine.GUI {

	public class ButtonRate : MonoBehaviour {

		public string itunesID;
		public string googlePlayID = "com.gemMine.woodChopper";
		string storeURL;


		public void OnRatingClicked() {
			#if UNITY_IPHONE
			storeURL = "https://itunes.apple.com/us/" + itunesID;
			#endif

			#if UNITY_ANDROID
			storeURL = "https://play.google.com/store/apps/details?id=" + googlePlayID;
			#endif			

			Application.OpenURL (storeURL);
		}
	}
}
