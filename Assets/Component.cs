using UnityEngine;
using System.Linq;
using System.Collections;

public class Component : MonoBehaviour {
	public GameObject attachedTo = null;
	public bool isAttached = false;
	protected Vector3 attachOffset;
	protected Animator anim;
	protected bool meleeAttacking = false;
	protected float attackSpeed = .5f;
	protected float nextMeleeAttack = 0f;
	// Use this for initialization
	void Awake () {
		// Turn off animation on spawn
		anim = this.GetComponentsInChildren<Animator>().FirstOrDefault();
		if (anim != null)
			anim.enabled = false;
	
	}
	
	// Update is called once per frame
	public void Update () {
		if (meleeAttacking && Time.time > nextMeleeAttack) {
			nextMeleeAttack = Time.time + attackSpeed;
			foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("creature")) {
				if (this.GetComponent<CircleCollider2D> ().IsTouching (enemy.GetComponent<Collider2D> ())) {
					GameObject.Destroy (enemy);
					attachedTo.SendMessage ("setscore");
				}
			}
		}
	}

	public virtual void attach(GameObject player) {
		isAttached = true;
		attachedTo = player;
		attachOffset = this.transform.position - player.transform.position;
		GetComponent<Rigidbody2D> ().isKinematic = true;

		// Set layer to "player" to avoid colliding with itself
		this.gameObject.layer = 8;
	}
}
	