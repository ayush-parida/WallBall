using UnityEngine;
using System.Collections;

//
// This Camera script follows the Player Object ("Sphere")

public class cameraScript : MonoBehaviour {

	// Player object's Transform
	public Transform target;

	Vector3 oldPosition;

	// Use this for initialization
	void Start () {
		// set up the camera
		setup ();
	}


	/// <summary>
	/// Sets up the camera
	/// </summary>
	/// 
	public void setup() {
		// do we follow a player object, actually?
		// if not, set the values
		if (target == null)
			target = GameObject.Find ("Sphere").transform;
		oldPosition = target.position;
	}

	// Update is called once per frame
	void Update () {
	
	}

	/// <summary>
	/// Updates the camera
	/// Always do camera updates in LateUpdate
	/// </summary>
	/// 
	void LateUpdate() {
		if (target != null) {
			Vector3 position = target.position;
			Vector3 delta = oldPosition - position;
			delta.y = 0;
			transform.position = transform.position - delta;
			oldPosition = position;
		}
	}
}
