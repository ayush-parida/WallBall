using UnityEngine;
using System.Collections;

//
// The gem consists of nested objects
// first of all, the gem is hovering
// this is achieved by a simple sine function

public class gemHover : MonoBehaviour {

	float startY;
	float hover = 0.1f;
	float speed = 2f;

	// Use this for initialization
	void Start () {
		startY = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 position = transform.position;
		position = new Vector3 (position.x, startY + Mathf.Sin(Time.time*speed)*0.1f, position.z);
		transform.position = position;
	}
}
