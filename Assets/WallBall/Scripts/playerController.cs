using UnityEngine;
using System.Collections;

//
// This script controls the player 
//

public class playerController : ManagerBase {

	// minimal physics and collision detection 
	// for the plaxer object
	Rigidbody rigidBody;
	// player can roll in two directions
	bool rollingLeft;
	// is player falling down?
	bool fallingDown;
	// player speed.
	// faster means more difficult
	public float speed=2;

	// Reference to the main gaming controller script
	gameScript gameScriptReference;

	// Use this for initialization
	void Start () {
		// get the rigidbody reference
		rigidBody = GetComponent<Rigidbody> ();
		// set starting direction to left
		rollingLeft = true;
		// the player is not falling
		fallingDown = false;
		// get reference to the main controller script
		gameScriptReference = GameObject.Find ("GameController").GetComponent<gameScript> ();
	}


	// Update is called once per frame
	void Update () {
		// only update the player if game is in game mode
		if (gameScriptReference.inGame()) {
			// if player presses space key or
			// taps on the screen, 
			// change the direction
			if (Input.GetKeyDown (KeyCode.Space) ||
			    Input.GetMouseButtonDown(0)) {
				gameScriptReference.addScore(1);
				rollingLeft = !rollingLeft;
			}
			// player is falling down
			// that means, the game is over
			if (transform.position.y < -10) {
				gameScriptReference.gameOver();
				Destroy(gameObject);
			}
			// Player begins to fall down.
			// play falling sound
			if (!fallingDown) {
				if (transform.position.y < -0.3f) {
					fallingDown = true;
					soundManager.playFallingSound();
					soundManager.PlayGameOverMusic ();
				}
			}
		}
	}


	void FixedUpdate() {
		// only update the player if game is in game mode
		// ball physics
		if (gameScriptReference.inGame()) {
			if (rollingLeft)
				rigidBody.velocity = new Vector3 (-speed, Physics.gravity.y, 0);
			else
				rigidBody.velocity = new Vector3 (0, Physics.gravity.y, speed);
		}
	}
}
