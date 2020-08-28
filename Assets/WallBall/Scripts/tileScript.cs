using UnityEngine;
using System.Collections;

// This script handles the lifecycle of a tile
// (the blue cubes)

public class tileScript : ManagerBase {

	// tiles can fall down
	// start position and end position of fall animation
	Vector3 startMarker;
	Vector3 endMarker;
	// fall speed, may be adjusted at will
	public float speed = 3.0F;
	private float startTime;
	private float journeyLength;
	// I wanted to make the falling tile transparent
	Color startCol, endCol;

	// a tile can have a gem
	public GameObject gem;

	bool triggerLeft;

	float timer;
	float timerInterval = 0.2f;

	// Use this for initialization
	void Start () {
		// Initialize the tile
		triggerLeft = false;
		startMarker = transform.position;
		endMarker = startMarker - new Vector3 (0, 10, 0);
		//startCol = transform.GetComponent<Renderer> ().material.color;
		//endCol = startCol;
		//endCol.a = 0;

		// generate a random value,
		// with a probability of 40% we will place a gem on the tile
		int myRand = Random.Range (0, 101);
		if (myRand < 40 && gameObject.name != "Plane") {
			GameObject gemGO=Instantiate(gem,transform.position,Quaternion.identity) as GameObject;
			gemGO.transform.position += new Vector3(0,2,0);
			gemGO.transform.SetParent(this.transform);
		}
	}
	
	// Update is called once per frame
	void Update () {
		// only update the falling animation,
		// when player left the tile
		if (triggerLeft) {
			timer += Time.deltaTime;
			//if (timer > timerInterval) {
				//float distCovered = (Time.time - startTime) * speed;
			float distCovered = (timer-timerInterval)*speed;
				float fracJourney = distCovered / journeyLength;
				transform.position = Vector3.Lerp (startMarker, endMarker, fracJourney);
				//transform.GetComponent<Renderer> ().material.color=  Color.Lerp(startCol,endCol,fracJourney);
				// if tile has fallen 98%, destroy it
				if (fracJourney > 0.98f)
                    Destroy(this.gameObject);
            //}
        }
	}

	/// <summary>
	/// Raises the trigger exit event.
	/// </summary>
	/// <param name="other">Other.</param>
	void OnTriggerExit(Collider other) {
		// The player left a tile
		// now it may fall down
		if (other.name == "Sphere") {
			startTime = Time.time;
			journeyLength = Vector3.Distance(startMarker, endMarker);
			triggerLeft = true;
			soundManager.playShatterSound();
		}
	}
}