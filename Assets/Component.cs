using UnityEngine;
using System.Collections;

public class Component : MonoBehaviour {
	protected GameObject attachedTo = null;
	protected bool isAttached = false;
	protected Vector3 attachOffset;
	protected Animator anim;
	// Use this for initialization
	void Awake () {
		// Turn off animation on spawn
		anim = this.GetComponentsInChildren<Animator>()[0];
		anim.enabled = false;
	
	}
	
	// Update is called once per frame
	public void Update () {
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
