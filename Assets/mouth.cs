using UnityEngine;
using System.Collections;

public class mouth : Component {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	new void Update () {
		if (isAttached) {
			if (Input.GetKeyDown ("space")) {
				anim.enabled = true;
			}
			if (Input.GetKeyUp ("space")) {
				anim.enabled = false;
			}
		}
	}
}
