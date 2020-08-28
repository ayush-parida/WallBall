using UnityEngine;
using System.Collections;

//
// this script rotates the gem,
// which is nested in gemHover
//

public class gemRotate : MonoBehaviour {

	float speed = 1f;
	float angle;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		angle = (angle + speed) % 360f;
		transform.localRotation = Quaternion.Euler(new Vector3(0,angle,0));
	}
}
