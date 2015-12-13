using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {
	public Transform body;
	public float speed;
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
		body.transform.Translate (mouseOffset () * speed * Time.deltaTime);
	}

	// Update is called once per frame
	void Update () {
		Move ();

		// Left click, attach component
		if (Input.GetMouseButtonDown (0)) {
			// Cycle through "component" objects, check for Collider2D collisions
			GameObject[] components = GameObject.FindGameObjectsWithTag ("component");
			Collider2D playerCollider = this.GetComponent<Collider2D> ();
			foreach (GameObject component in components) {
				Collider2D componentCollider = component.GetComponent<Collider2D> ();
				if (playerCollider.IsTouching (componentCollider)) {
					component.SendMessage ("attach", this.gameObject);
					this.attachedComponents.Add (component);
				}
			}
		}

	}
}
