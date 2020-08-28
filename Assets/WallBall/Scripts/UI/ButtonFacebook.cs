using UnityEngine;
using System.Collections;

// This script is attached to the "Like" Button in the main menu
// It opens up Facebook, if clicked

namespace GemMine.GUI {

	public class ButtonFacebook : MonoBehaviour {

		//
		// enter your facebook profile id here
		//

		public string facebookprofile = "gemminemedia";


		//
		// void OnFacebookClicked() 
		//
		// Open up facebook
		// if installed, open up the app
		// open up browser, if the app is not installed
		//

		public void OnFacebookClicked() {
			Application.OpenURL ("https://www.facebook.com/n/?" + facebookprofile);
		}
	}
}
