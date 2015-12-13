using UnityEngine;
using System.Collections;

public class mouth : Component {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	new void Update () {
		base.Update ();
		if (isAttached) {
			if (Input.GetKeyDown ("space")) {
				anim.enabled = true;
				meleeAttacking = true;
			}
			if (Input.GetKeyUp ("space")) {
				anim.enabled = false;
				meleeAttacking = false;
			}
		}

		this.transform.RotateAround (this.transform.position,
			Vector3.forward,
			1 * Time.deltaTime);
	}

	public override void attach (GameObject player)
	{
		var dir = transform.position - player.transform.position;

		// always point outwards
		transform.RotateAround (
			transform.position,
			Vector3.forward,
			Vector3.Angle(transform.localRotation * Vector3.right, dir.normalized)
		);

		// fixed radius
		transform.position = player.transform.position + dir.normalized * 2.5f;
		base.attach (player);
	}
}
