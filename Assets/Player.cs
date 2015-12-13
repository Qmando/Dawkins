using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Player : MonoBehaviour {
	
	public float size = 1.0f;
	public const float SIZE_CONST = 0.1f;

	public Transform body;
	public float speed;
	public float rotateSpeed;
	private List<GameObject> attachedComponents = new List<GameObject>();

	// Use this for initialization
	void Start () {

	}

	public static Vector2 mouseOffset () {
		float x = Input.mousePosition.x - Screen.width/2;
		float y = Input.mousePosition.y - Screen.height/2;
		return new Vector2 (x, y);
	}

	void Move () {
		Vector2 mouse = mouseOffset();
		float addedSpeed = 0f;
		if (mouse.magnitude > 15 && mouse.magnitude < 150) {
			addedSpeed = 120f;
		}
		body.transform.Translate (((mouse.normalized * addedSpeed) + mouse) * speed / 200f * Time.deltaTime, Space.World);

		// If mouse is away from center

	
		float angle = Vector2.Angle (Vector2.right, mouseOffset ().normalized);
		if (mouse.y < 0) {
			angle = 360 - angle;
		}
		Vector3 rot = body.rotation.eulerAngles;
		angle = angle - rot.z;
		if (angle > 180) {
			angle = angle - 360;
		} else if (angle < -180) {
			angle = angle + 360;
		}
		if (!(angle > 145 || angle < -145f || mouse.magnitude < 150)) {
			if (angle < 0) {
				angle = Mathf.Min (angle, 5f);
			} else {
				angle = Mathf.Max (angle, -5f);
			}
			angle *= rotateSpeed * Time.deltaTime;
			body.Rotate (new Vector3 (0, 0, angle));
		}


		//Quaternion targetRot = new Quaternion (0, 0, angle, 0);
		//body.rotation = Quaternion.Slerp(body.rotation, targetRot, Time.deltaTime * 1);
	
	}

	void addSpeed() {
		this.speed += .3f;
		this.rotateSpeed += .2f;
	}

	void decreaseSpeed() {
		this.speed -= .3f;
		this.rotateSpeed -= .2f;
	}

	// Update is called once per frame
	void Update () {

		body.transform.localScale.Set (size, size, size);
		Move ();

		// Left click, attach component
		if (Input.GetMouseButtonDown (0)) {
			// Cycle through "component" objects, check for Collider2D collisions
			GameObject[] components = GameObject.FindGameObjectsWithTag ("component");
			Collider2D playerCollider = this.GetComponent<Collider2D> ();
			List<Collider2D> bodyParts = (from part in this.attachedComponents
			                         where part.name == "bodyCell"
									select part.GetComponentInChildren<Collider2D> () ).ToList<Collider2D>();
			bodyParts.Add (playerCollider);
			foreach (GameObject component in components) {
				foreach (Collider2D componentCollider in component.GetComponentsInChildren<Collider2D> ()) {
					if (!componentCollider.isTrigger) {
						continue;
					}
					foreach (Collider2D bodyCollider in bodyParts) {
						if (bodyCollider.IsTouching (componentCollider)) {
							component.SendMessage ("attach", this.gameObject);
							this.attachedComponents.Add (component);
							component.transform.SetParent (this.transform);
							break;
						}
					}
				}
			}
		}
	}

	void Consume(GameObject enemy) { 
		this.size += SIZE_CONST;
	}

	public void OnCollisionEnter(Collision col) { 
		Debug.LogError (col);
	}
}
