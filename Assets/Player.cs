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

			var colliders = 
				from component in GameObject.FindGameObjectsWithTag ("component")
				from collider in component.GetComponentsInChildren<Collider2D> ()
				where collider.isTrigger
				select new { Object = component, Collider = collider};

			var bodyParts = 
				from part in attachedComponents
				where part.name == "bodyCell"
				from component in part.GetComponentsInChildren<Collider2D> ()
				select component;

			foreach (var bodypart in bodyParts.Concat(new[] { this.GetComponent<Collider2D>() }))
				foreach (var colliderInfo in colliders) 
					if (bodypart.IsTouching(colliderInfo.Collider)) { 
						colliderInfo.Object.SendMessage ("attach", this.gameObject);
						this.attachedComponents.Add (colliderInfo.Object);
						colliderInfo.Object.transform.SetParent (this.transform);
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
