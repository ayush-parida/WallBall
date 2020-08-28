using UnityEngine;
using System.Collections;

//
// This script handles the collision detection 
// between gem and player
//

public class gemScript : ManagerBase {

	// a reference to the main gaming controller script
	gameScript gameScriptReference;

	// sparkle a particel system
	// when player collects a gem
	public ParticleSystem sparkle;

	// Use this for initialization
	void Start () {
		// get the reference to the main gaming script
		gameScriptReference = GameObject.Find ("GameController").GetComponent<gameScript> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/// <summary>
	/// Raises the trigger enter event.
	/// </summary>
	/// <param name="other">Other.</param>
	void OnTriggerEnter(Collider other) {
		// gem collided with a plaer object
		if (other.name == "Sphere") {
			// play collect sound
			soundManager.playGemSound();
			// give the player 5 points
			gameScriptReference.addScore(5);
			// instantiate the particle system
			// you can find it in the folder "prefabs"
			ParticleSystem particleSystem = Instantiate(sparkle, transform.position, Quaternion.identity) as ParticleSystem;
			// deactivate the gem
			// it will be deleted by the tile which it is parented to
			this.gameObject.SetActive(false);
		}
	}
}
