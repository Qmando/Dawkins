using UnityEngine;
using System.Collections;

public class Component : MonoBehaviour {
	private GameObject attachedTo = null;
	private bool isAttached = false;
	private Vector3 attachOffset;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (isAttached) {
			this.transform.position = attachedTo.transform.position + attachOffset;
		}
	}

	void attach(GameObject player) {
		isAttached = true;
		attachedTo = player;
		attachOffset = this.transform.position - player.transform.position;

		// Set layer to "player" to avoid colliding with itself
		this.gameObject.layer = 8;
	}
}
