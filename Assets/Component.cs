using UnityEngine;
using System.Collections;

public class Component : MonoBehaviour {
	private GameObject attachedTo = null;
	private bool isAttached = false;
	private Vector3 attachOffset;
	Animator anim;
	// Use this for initialization
	void Awake () {
		// Turn off animation on spawn
		anim = this.GetComponentsInChildren<Animator>()[0];
		anim.enabled = false;
	
	}
	
	// Update is called once per frame
	void Update () {
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

	void attach(GameObject player) {
		isAttached = true;
		attachedTo = player;
		attachOffset = this.transform.position - player.transform.position;
		GetComponent<Rigidbody2D> ().isKinematic = true;


		// Set layer to "player" to avoid colliding with itself
		this.gameObject.layer = 8;
	}
}
