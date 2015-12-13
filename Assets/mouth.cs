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

	
	}

	public override void attach (GameObject player)
	{
		var dir = transform.position - player.transform.position;
		dir.z = 0;

		// always point outwards
		float moveAngle = Vector3.Angle(transform.rotation * Vector3.right, dir.normalized);
		Debug.Log (Vector3.Cross (transform.rotation * Vector3.right, dir.normalized));
		if (Vector3.Cross (transform.rotation * Vector3.right, dir.normalized).y > 0) {
			moveAngle = -moveAngle;
		}

		transform.RotateAround (
			transform.position,
			Vector3.forward,
			moveAngle
		);

		// fixed radius
		transform.position = player.transform.position + dir.normalized * 2.5f;
		base.attach (player);
	}
}
