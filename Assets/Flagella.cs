using UnityEngine;
using System.Collections;

public class Flagella : Component {

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	new void Update () {
		base.Update ();
		if (isAttached) {


			// Flagella, to be refactored
			Vector2 movement = Player.mouseOffset();
			float zRot = this.transform.rotation.eulerAngles.z;
			zRot += 90;
			zRot *= Mathf.Deg2Rad;
			Vector3 direction = transform.rotation * transform.up;
			Vector2 dir = new Vector2 (Mathf.Cos (zRot), Mathf.Sin (zRot));
			Vector3 heading = new Vector2 (movement.x, movement.y).normalized;
			float offset = Vector3.Dot (heading, dir);
			if (offset > .5 && !anim.enabled) {
				anim.enabled = true;
				attachedTo.SendMessage ("addSpeed");
			} else if (offset < .5 && anim.enabled) {
				anim.enabled = false;
				attachedTo.SendMessage ("decreaseSpeed");
			}
		}

	}
}
